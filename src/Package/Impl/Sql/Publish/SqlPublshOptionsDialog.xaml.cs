﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Common.Core;
using Microsoft.Common.Core.IO;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.R.Package.Commands;
using Microsoft.VisualStudio.R.Package.ProjectSystem;
using Microsoft.VisualStudio.R.Package.ProjectSystem.Configuration;
using Microsoft.VisualStudio.R.Package.Shell;
using Microsoft.VisualStudio.R.Packages.R;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudio.R.Package.Sql.Publish {
    /// <summary>
    /// Interaction logic for SqlPublsh.xaml
    /// </summary>
    public partial class SqlPublshOptionsDialog : DialogWindow {
        private readonly IApplicationShell _appShell;
        private readonly IProjectSystemServices _pss;
        private readonly IProjectConfigurationSettingsProvider _pcsp;
        private SqlPublishOptionsDialogViewModel _model;

        public static async Task<SqlPublshOptionsDialog> CreateAsync(
            IApplicationShell appShell, IProjectSystemServices pss, 
            IProjectConfigurationSettingsProvider pcsp) {
            var dialog = new SqlPublshOptionsDialog(appShell, pss, pcsp);
            await dialog.InitializeAsync();
            return dialog;
        }

        private SqlPublshOptionsDialog(IApplicationShell appShell, IProjectSystemServices pss, IProjectConfigurationSettingsProvider pcsp) :
            this(appShell, pss, new FileSystem(), pcsp) {
            InitializeComponent();

            TargetTypeList.SelectedIndex = _model.SelectedTargetTypeIndex;
            TargetList.SelectedIndex = _model.SelectedTargetIndex;
            CodePlacementList.SelectedIndex = _model.SelectedQuoteTypeIndex;
            QuoteTypeList.SelectedIndex = _model.SelectedQuoteTypeIndex;
        }

        private SqlPublshOptionsDialog(IApplicationShell appShell, IProjectSystemServices pss, IFileSystem fs, IProjectConfigurationSettingsProvider pcsp) {
            _appShell = appShell;
            _pss = pss;
            _pcsp = pcsp;

            Title = Package.Resources.SqlPublishDialog_Title;
        }

        private async Task InitializeAsync() {
            var settings = new SqlSProcPublishSettings(_appShell.SettingsStorage);
            _model = await SqlPublishOptionsDialogViewModel.CreateAsync(settings, _appShell, _pss, _pcsp);
            DataContext = _model;
        }

        private void SaveSettingsAndClose() {
            _model.Settings.Save(_appShell.SettingsStorage);

            var uiShell = _appShell.GetGlobalService<IVsUIShell>(typeof(SVsUIShell));
            var guid = RGuidList.RCmdSetGuid;
            var o = new object();
            uiShell.PostExecCommand(ref guid, RPackageCommandId.icmdPublishSProc, 0, ref o);
            Close();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e) => SaveSettingsAndClose();
        private void CancelButton_Click(object sender, RoutedEventArgs e) => Close();
        private void TableName_TextChanged(object sender, TextChangedEventArgs e) => _model.UpdateState();

        private void TargetTypeList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            _model.SelectTargetTypeAsync(TargetTypeList.SelectedIndex).ContinueWith(t => {
                _appShell.DispatchOnUIThread(() => TargetList.SelectedIndex = _model.SelectedTargetIndex);
            }).DoNotWait();
        }

        private void TargetList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            _model.SelectTarget(TargetList.SelectedIndex);
        }

        private void CodePlacementList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            _model.SelectCodePlacement(CodePlacementList.SelectedIndex);
        }

        private void QuoteTypeList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            _model.SelectQuoteType(QuoteTypeList.SelectedIndex);
        }
    }
}

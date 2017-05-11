﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Runtime.InteropServices;
using Microsoft.Common.Core.Shell;
using Microsoft.R.Components.InfoBar;
using Microsoft.R.Components.PackageManager;
using Microsoft.R.Components.PackageManager.Implementation;
using Microsoft.R.Components.Search;
using Microsoft.R.Components.Settings;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.R.Package.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Constants = Microsoft.VisualStudio.OLE.Interop.Constants;

namespace Microsoft.VisualStudio.R.Package.ToolWindows {
    [Guid(WindowGuidString)]
    internal class PackageManagerWindowPane : VisualComponentToolWindow<IRPackageManagerVisualComponent>, IOleCommandTarget {
        private readonly IRPackageManager _packageManager;
        private readonly ISearchControlProvider _searchControlProvider;
        private readonly IInfoBarProvider _infoBarProvider;
        private readonly IRSettings _settings;
        private readonly ICoreShell _coreShell;
        public const string WindowGuidString = "363F84AD-3397-4FDE-97EA-1ABD73C64BB3";
        public static Guid WindowGuid { get; } = new Guid(WindowGuidString);

        private IOleCommandTarget _commandTarget;

        public PackageManagerWindowPane(IRPackageManager packageManager, ISearchControlProvider searchControlProvider, IInfoBarProvider infoBarProvider, IRSettings settings, ICoreShell coreShell) {
            _packageManager = packageManager;
            _searchControlProvider = searchControlProvider;
            _infoBarProvider = infoBarProvider;
            _settings = settings;
            _coreShell = coreShell;
            BitmapImageMoniker = KnownMonikers.Package;
            Caption = Resources.PackageManagerWindowCaption;
        }

        protected override void OnCreate() {
            Component = new RPackageManagerVisualComponent(_packageManager, this, _searchControlProvider, _infoBarProvider, _settings, _coreShell);
            base.OnCreate();
        }

        public override void OnToolWindowCreated() {
            Guid commandUiGuid = VSConstants.GUID_TextEditorFactory;
            ((IVsWindowFrame)Frame).SetGuidProperty((int)__VSFPROPID.VSFPROPID_InheritKeyBindings, ref commandUiGuid);
            base.OnToolWindowCreated();
        }

        protected override void Dispose(bool disposing) {
            if (disposing && Component != null) {
                Component.Dispose();
                Component = null;
                _commandTarget = null;
            }
            base.Dispose(disposing);
        }

        public int QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText) {
            return _commandTarget?.QueryStatus(ref pguidCmdGroup, cCmds, prgCmds, pCmdText) ?? (int)Constants.OLECMDERR_E_NOTSUPPORTED;
        }

        public int Exec(ref Guid pguidCmdGroup, uint nCmdId, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut) {
            return _commandTarget?.Exec(ref pguidCmdGroup, nCmdId, nCmdexecopt, pvaIn, pvaOut) ?? (int)Constants.OLECMDERR_E_NOTSUPPORTED;
        }
    }
}

﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.Languages.Editor.Tests.Shell;
using Microsoft.R.Editor.ContentType;
using Microsoft.VisualStudio.Editor.Mocks;
using Microsoft.VisualStudio.R.Package.Repl;
using Microsoft.VisualStudio.R.Package.Shell;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.VisualStudio.R.Package.Test.Repl {
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ReplWindowProviderTest {
        [TestMethod]
        public void ReplWindowProvider_ConstructionTest() {
            AppShell.Current = TestAppShell.Current;
            RInteractiveWindowProvider provider = new RInteractiveWindowProvider();
        }

        [TestMethod]
        public void ReplWindowProvider_InteractiveWindowCreateTest() {
            AppShell.Current = TestAppShell.Current;

            RInteractiveWindowProvider provider = new RInteractiveWindowProvider();
            ITextBufferFactoryService svc = AppShell.Current.ExportProvider.GetExportedValue<ITextBufferFactoryService>();
            IContentTypeRegistryService r = AppShell.Current.ExportProvider.GetExportedValue<IContentTypeRegistryService>();
            ITextBuffer buffer = svc.CreateTextBuffer(r.GetContentType(RContentTypeDefinition.ContentType));

            var window = provider.Create(0, new TextViewMock(buffer));
            Assert.IsNotNull(window);
        }
    }
}

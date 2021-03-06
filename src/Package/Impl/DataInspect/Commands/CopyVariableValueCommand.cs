// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Threading.Tasks;

namespace Microsoft.VisualStudio.R.Package.DataInspect.Commands {
    internal class CopyVariableValueCommand : VariableCommandBase {
        public CopyVariableValueCommand(VariableView variableView) : base(variableView) {}

        protected override bool IsEnabled(VariableViewModel variable) => true;

        protected override Task InvokeAsync(VariableViewModel variable) {
            VariableView.CopyValue(variable);
            return Task.CompletedTask;
        }
    }
}
﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.WindowsAzure.Commands.ServiceManagement.IaaS.Extensions
{
    using System;
    using System.Management.Automation;
    using Model;
    using Properties;

    [Cmdlet(
        VerbsCommon.Set,
        VirtualMachineEnableAccessExtensionNoun,
        DefaultParameterSetName = EnableExtensionWithNewOrExistingCredentialParameterSet),
    OutputType(
        typeof(IPersistentVM))]
    public class SetAzureVMEnableAccessExtensionCommand : VirtualMachineEnableAccessExtensionCmdletBase
    {
        public const string EnableExtensionWithNewOrExistingCredentialParameterSet = "EnableExtensionWithNewOrExistingCredential";
        public const string DisableExtensionParameterSet = "DisableExtension";

        [Parameter(
            ParameterSetName = EnableExtensionWithNewOrExistingCredentialParameterSet,
            Mandatory = false,
            Position = 1,
            HelpMessage = "New or Existing User Name")]
        public override string UserName
        {
            get;
            set;
        }

        [Parameter(
            ParameterSetName = EnableExtensionWithNewOrExistingCredentialParameterSet,
            Mandatory = false,
            Position = 2,
            HelpMessage = "New or Existing User Password")]
        public override string Password
        {
            get;
            set;
        }

        [Parameter(
            ParameterSetName = DisableExtensionParameterSet,
            Mandatory = true,
            Position = 1,
            HelpMessage = "Disable VM Access Extension")]
        public override SwitchParameter Disable
        {
            get;
            set;
        }

        internal void ExecuteCommand()
        {
            var extensionRef = GetPredicateExtension();
            if (extensionRef != null)
            {
                SetResourceExtension(extensionRef);
            }
            else
            {
                AddResourceExtension();
            }

            WriteObject(VM);
        }

        protected override void ValidateParameters()
        {
            base.ValidateParameters();
            this.PublicConfiguration = GetEnableVMAccessAgentConfig();
        }

        protected override void ProcessRecord()
        {
            ServiceManagementProfile.Initialize();
            try
            {
                base.ProcessRecord();
                ExecuteCommand();
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(ex, string.Empty, ErrorCategory.CloseError, null));
            }
        }
    }
}

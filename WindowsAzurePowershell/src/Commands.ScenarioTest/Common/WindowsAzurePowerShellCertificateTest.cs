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


namespace Microsoft.WindowsAzure.Commands.ScenarioTest.Common
{
    using System;
    using System.Collections.Generic;
    using VisualStudio.TestTools.UnitTesting;
    using Commands.Common;
    using Microsoft.WindowsAzure.Utilities.HttpRecorder;
    using Microsoft.WindowsAzure.Commands.Utilities.Common;

    [TestClass]
    public class WindowsAzurePowerShellCertificateTest : PowerShellTest
    {
        protected TestCredentialHelper credentials;
        
        protected string credentialFile;

        protected string profileFile;
        
        protected List<HttpMockServer> mockServers;

        private void OnClientCreated(object sender, ClientCreatedArgs e)
        {
            HttpMockServer mockServer = new HttpMockServer(new SimpleRecordMatcher());
            e.AddHandlerToClient(mockServer);
            mockServers.Add(mockServer);
        }

        public WindowsAzurePowerShellCertificateTest(params string[] modules)
            : base(modules)
        {
            this.credentials = new TestCredentialHelper(Environment.CurrentDirectory);
            this.credentialFile = TestCredentialHelper.DefaultCredentialFile;
            this.profileFile = TestCredentialHelper.WindowsAzureProfileFile;
        }

        [TestInitialize]
        public override void TestSetup()
        {
            base.TestSetup();
            this.mockServers = new List<HttpMockServer>();
            WindowsAzureSubscription.OnClientCreated += OnClientCreated;
            this.credentials.SetupPowerShellEnvironment(powershell, this.credentialFile, this.profileFile);
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
            {
                return true;
            };

        }

        [TestCleanup]
        public override void TestCleanup()
        {
            base.TestCleanup();
            WindowsAzureSubscription.OnClientCreated -= OnClientCreated;
            mockServers.ForEach(ms => ms.Dispose());
        }
    }
}
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

using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.WindowsAzure.Commands.Common.Utilities;

namespace Microsoft.Azure.Commands.Resources.Models.ActiveDirectory
{
    public class PSADObject
    {
        public string DisplayName { get; set; }

        public Guid Id { get; set; }

        public string Type { get; set; }

        public Dictionary<string, string> Properties;

        public PSADObject()
        {
            Properties = new Dictionary<string, string>();
        }

        public string GetProperty(string property)
        {
            return Properties.GetProperty(property);
        }

        public string[] GetPropertyAsArray(string property)
        {
            return Properties.GetPropertyAsArray(property);
        }

        public void SetProperty(string property, params string[] values)
        {
            Properties.SetProperty(property, values);
        }

        public void SetOrAppendProperty(string property, params string[] values)
        {
            Properties.SetOrAppendProperty(property, values);
        }

        public bool IsPropertySet(string property)
        {
            return Properties.IsPropertySet(property);
        }
    }
}

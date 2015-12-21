// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------------------- 

using System;

namespace Microsoft.Azure.Mobile.Server.Files
{
    public class StorageTokenRequest
    {
        public StoragePermissions Permissions { get; set; }

        public string ProviderName { get; set; }

        public MobileServiceFile TargetFile { get; set; }
    }

}

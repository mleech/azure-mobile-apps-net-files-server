// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------------------- 

using System;

namespace Microsoft.Azure.Mobile.Server.Files
{
    public class StorageToken
    {
        public string RawToken { get; set; }

        public Uri ResourceUri { get; set; }

        public string EntityId { get; set; }

        public StoragePermissions Permissions { get; set; }

        public StorageTokenScope Scope { get; set; }
    }
}

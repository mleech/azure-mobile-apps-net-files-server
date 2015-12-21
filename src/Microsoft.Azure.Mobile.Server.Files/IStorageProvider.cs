// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Azure.Mobile.Server.Files
{
    public abstract class StorageProvider
    {
        abstract public string Name { get; }

        abstract public Task<IEnumerable<MobileServiceFile>> GetRecordFilesAsync(string tableName, string recordId, IContainerNameResolver containerNameResolver);

        abstract public Task DeleteFileAsync(string tableName, string recordId, string fileName, IContainerNameResolver containerNameResolver);

        abstract public Task<StorageToken> GetAccessTokenAsync(StorageTokenRequest request, StorageTokenScope scope, IContainerNameResolver containerNameResolver);
    }
}

// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.Azure.Mobile.Server.Files;
using Microsoft.Azure.Mobile.Server.Files.Properties;

namespace Microsoft.Azure.Mobile.Server.Files.Controllers
{
    public abstract class StorageController<T> : ApiController
    {
        private StorageProvider storageProvider;

        public StorageController()
            : this(Constants.StorageConnectionStringName)
        { }

        public StorageController(string connectionStringName)
        {
            if (connectionStringName == null)
            {
                throw new ArgumentNullException("connectionStringName");
            }

            if (connectionStringName.Length == 0)
            {
                throw new ArgumentException(Resources.ConnectionStringNameMayNotBeEmpty);
            }

            var connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];

            if (connectionStringSettings == null)
            {
                throw new ConfigurationErrorsException(string.Format(Resources.MissingConnectionString, connectionStringName));
            }

            this.storageProvider = new AzureStorageProvider(connectionStringSettings.ConnectionString);
        }

        public StorageController(StorageProvider storageProvider)
        {
            if (storageProvider == null)
            {
                throw new ArgumentNullException("storageProvider");
            }

            this.storageProvider = storageProvider;
        }

        public Task<StorageToken> GetStorageTokenAsync(string id, StorageTokenRequest value)
        {
            return GetStorageTokenAsync(id, value, new ContainerNameResolver());
        }

        public virtual async Task<StorageToken> GetStorageTokenAsync(string id, StorageTokenRequest value, IContainerNameResolver containerNameResolver)
        {
            StorageTokenScope scope = GetStorageScopeForRequest(id, value);

            StorageToken token = await this.storageProvider.GetAccessTokenAsync(value, scope, containerNameResolver);

            return token;
        }

        public Task<IEnumerable<MobileServiceFile>> GetRecordFilesAsync(string id)
        {
            return GetRecordFilesAsync(id, new ContainerNameResolver());
        }

        public async Task<IEnumerable<MobileServiceFile>> GetRecordFilesAsync(string id, IContainerNameResolver containerNameResolver)
        {
            return await this.storageProvider.GetRecordFilesAsync(GetTableName(), id, containerNameResolver);
        }

        protected virtual StorageTokenScope GetStorageScopeForRequest(string id, StorageTokenRequest value)
        {
            return StorageTokenScope.Record;
        }

        private string GetTableName()
        {
            // TODO: This works for this tests, but we need to use the same logic applied by the framework to get the table name.
            return typeof(T).Name;
        }

        public Task DeleteFileAsync(string id, string name)
        {
            return DeleteFileAsync(id, name, new ContainerNameResolver());
        }

        public async Task DeleteFileAsync(string id, string name, IContainerNameResolver containerNameResolver)
        {
            // Validate user and request
            await this.storageProvider.DeleteFileAsync(GetTableName(), id, name, containerNameResolver);
        }

        protected virtual bool IsTokenRequestValid(StorageTokenRequest request, ClaimsPrincipal user)
        {
            return true;
        }
    }
}

// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using Microsoft.Azure.Mobile.Server.Files.Properties;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace Microsoft.Azure.Mobile.Server.Files
{
    public class MobileServiceFile
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string TableName { get; set; }

        public virtual string ParentId { get; set; }

        public virtual string ContentMD5 { get; set; }

        public virtual long Length { get; set; }

        public DateTimeOffset? LastModified { get; set; }

        public string StoreUri { get; set; }

        public virtual IDictionary<string, string> Metadata { get; set; }

        public static MobileServiceFile FromBlobItem(CloudBlockBlob item, string parentEntityType, string parentEntityId)
        {
            return new MobileServiceFile
            {
                Id = item.Name,
                Name = item.Name,
                TableName = parentEntityType,
                ParentId = parentEntityId,
                Length = item.Properties.Length,
                ContentMD5 = item.Properties.ContentMD5,
                LastModified = item.Properties.LastModified,
                Metadata = item.Metadata,
                StoreUri = item.Uri.LocalPath
            };
        }

        [JsonIgnore]
        public virtual string FileInfoToken
        {
            get
            {
                return JsonConvert.SerializeObject(this);
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }
                try
                {
                    JsonConvert.PopulateObject(value, this,
                        new JsonSerializerSettings
                        {
                            Error = (a, s) => s.ErrorContext.Handled = true
                        });
                }
                catch { }
            }
        }

        private class NullMobileServiceFile : MobileServiceFile
        {
            public override string TableName
            {
                get
                {
                    return null;
                }
                set { throw new InvalidOperationException(Resources.CannotModifyMobileFileInstance); }
            }

            public override string ContentMD5
            {
                get
                {
                    return null;
                }
                set { throw new InvalidOperationException(Resources.CannotModifyMobileFileInstance); }
            }

            public override string FileInfoToken
            {
                get
                {
                    return null;
                }
                set { throw new InvalidOperationException(Resources.CannotModifyMobileFileInstance); }
            }

            public override string Id
            {
                get
                {
                    return null;
                }
                set { throw new InvalidOperationException(Resources.CannotModifyMobileFileInstance); }
            }

            public override long Length
            {
                get
                {
                    return 0;
                }
                set { throw new InvalidOperationException(Resources.CannotModifyMobileFileInstance); }
            }

            public override IDictionary<string, string> Metadata
            {
                get
                {
                    return null;
                }
                set { throw new InvalidOperationException(Resources.CannotModifyMobileFileInstance); }
            }

            public override string Name
            {
                get
                {
                    return null;
                }
                set { throw new InvalidOperationException(Resources.CannotModifyMobileFileInstance); }
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.WindowsAzure.MobileServices.Files
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

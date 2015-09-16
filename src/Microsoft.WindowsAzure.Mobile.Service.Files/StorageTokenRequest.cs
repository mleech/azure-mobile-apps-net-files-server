using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Mobile.Service.Files;

namespace Microsoft.WindowsAzure.MobileServices.Files
{
    public class StorageTokenRequest
    {
        public StoragePermissions Permissions { get; set; }

        public string ProviderName { get; set; }

        public MobileServiceFile TargetFile { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.WindowsAzure.MobileServices.Files
{
    public interface IContainerNameResolver
    {
        Task<string> GetFileContainerNameAsync(string tableName, string recordId, string fileName);

        Task<IEnumerable<string>> GetRecordContainerNames(string tableName, string recordId);
    }

}

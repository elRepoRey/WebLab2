using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TheFlowerplace.Services
{
    public interface IFileService
    {
        Task UploadFileAsync(string containerName, string fileName, Stream content);
                
        Task<IEnumerable<string>> ListAllFilesOnBlobStorage(string containerName);

       
        Task UploadFolderAsync(string containerName, string folderPath);

        
        Task DeleteFileAsync(string containerName, string fileName);
    }
}
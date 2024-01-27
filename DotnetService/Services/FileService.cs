using Azure.Storage.Blobs;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TheFlowerplace.Services
{
  
    public class FileService : IFileService
    {
        private readonly BlobServiceClient _blobServiceClient;

       
        public FileService(string connectionString)
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task UploadFileAsync(string containerName, string fileName, Stream content)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(containerName);
            await blobContainer.CreateIfNotExistsAsync();
            var blobClient = blobContainer.GetBlobClient(fileName);

            await blobClient.UploadAsync(content, true);
        }

        public async Task<IEnumerable<string>> ListAllFilesOnBlobStorage(string containerName)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(containerName);
            var items = new List<string>();

            await foreach (var blobItem in blobContainer.GetBlobsAsync())
            {
                items.Add(blobItem.Name);
            }

            return items;
        }

        
        public async Task<Stream> GetFileAsync(string containerName, string fileName)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainer.GetBlobClient(fileName);

            var stream = new MemoryStream();
            await blobClient.DownloadToAsync(stream);
            stream.Position = 0;

            return stream;
        }

          
        public async Task DeleteFileAsync(string containerName, string fileName)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainer.GetBlobClient(fileName);

            await blobClient.DeleteIfExistsAsync();
        }

       
        public async Task UploadFolderAsync(string containerName, string folderPath)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(containerName);
            await blobContainer.CreateIfNotExistsAsync();

            foreach (var filePath in Directory.GetFiles(folderPath))
            {
                var fileName = Path.GetFileName(filePath);
                var blobClient = blobContainer.GetBlobClient(fileName);
                using var stream = File.OpenRead(filePath);
                await blobClient.UploadAsync(stream, true);
            }
        }

    }
}

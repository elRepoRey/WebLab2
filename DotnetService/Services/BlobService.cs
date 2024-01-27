using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using System;

namespace TheFlowerplace.Services
{
   
    public class BlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

      
        public BlobService(string connectionString)
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        
        public string GenerateBlobReadSasUri(string containerName, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            var sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = containerName,
                BlobName = fileName,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5),
                ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
            };

            sasBuilder.SetPermissions(BlobSasPermissions.Read);

            var sasUri = blobClient.GenerateSasUri(sasBuilder);
            return sasUri.ToString();
        }
    }
}

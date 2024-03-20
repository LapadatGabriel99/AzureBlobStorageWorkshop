using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Image.Api.Extensions;
using Image.Api.Models;
using Image.Api.Services.Contracts;

namespace Image.Api.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<BlobFileResult> DownloadBlob(string containerName, string blobName)
        {
            var blobContainerClient = await _blobServiceClient.GetBlobContainerClientAsync(containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            var blobDownloadResult = (await blobClient.DownloadContentAsync()).Value;

            if (blobDownloadResult is null)
            {
                throw new Exception("Unable to download blob!");
            }

            var blobFileResult = new BlobFileResult
            {
                FileName = blobName,
                ContentType = blobDownloadResult.Details.ContentType,
                FileStream = blobDownloadResult.Content.ToStream()
            };

            return blobFileResult;
        }

        public async Task<bool> UploadBlob(string containerName, string blobName, Stream fileStream, string contentType)
        {
            var blobContainerClient = await _blobServiceClient.GetBlobContainerClientAsync(containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);
            var blobHttpHeaders = new BlobHttpHeaders
            {
                ContentType = contentType
            };

            var result = await blobClient.UploadAsync(fileStream, blobHttpHeaders);

            return result is not null;
        }
    }
}

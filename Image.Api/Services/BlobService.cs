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

            var blobDownloadResult = await blobClient.DownloadContentAsync();

            if (blobDownloadResult.GetRawResponse().IsError)
            {
                throw new Exception("Unable to download blob!");
            }

            var blobContent = blobDownloadResult.Value;

            var blobFileResult = new BlobFileResult
            {
                FileName = blobName,
                ContentType = blobContent.Details.ContentType,
                FileStream = blobContent.Content.ToStream()
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

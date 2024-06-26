﻿using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Image.Api.Extensions;
using Image.Api.Models;
using Image.Api.Services.Contracts;

namespace Image.Api.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ISecretService _secretService;

        public BlobService(BlobServiceClient blobServiceClient, ISecretService secretService)
        {
            _blobServiceClient = blobServiceClient;
            _secretService = secretService;
        }

        public async Task<BlobFileResult> DownloadBlob(string containerName, string blobName)
        {
            var blobContainerClient = await _blobServiceClient.GetBlobContainerClientAsync(containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            var blobDownloadResult = await blobClient.DownloadContentAsync();

            if (blobDownloadResult.GetRawResponse().IsError)
            {
                throw new Exception($"Unable to download blob {blobName}!");
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

        public async Task<string> RetrieveBlob(string containerName, string blobName)
        {
            var blobContainerClient = await _blobServiceClient.GetBlobContainerClientAsync(containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            return await blobClient.GetBlobUri(_secretService);  
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

        public async Task<bool> DeleteBlob(string containerName, string blobName)
        {
            var blobContainerClient = await _blobServiceClient.GetBlobContainerClientAsync(containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            var blobDeleteResult = await blobClient.DeleteIfExistsAsync();

            if (blobDeleteResult.GetRawResponse().IsError)
            {
                throw new Exception($"Unable to delete blob {blobName}!");
            }

            return blobDeleteResult.Value;
        }
    }
}

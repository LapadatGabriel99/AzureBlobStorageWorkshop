using Azure.Storage.Blobs;

namespace Image.Api.Extensions
{
    public static class BlobServiceClientExtensions
    {
        public static async Task<BlobContainerClient> GetBlobContainerClientAsync(this BlobServiceClient blobServiceClient, string containerName)
        {
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await blobContainerClient.CreateIfNotExistsAsync();

            return blobContainerClient;
        }
    }
}

using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Image.Api.Services.Contracts;

namespace Image.Api.Extensions
{
    public static class BlobClientExensions
    {
        public static async Task<string> GetBlobUri(this BlobClient blobClient, ISecretService secretService)
        {
            BlobSasBuilder sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = blobClient.BlobContainerName,
                BlobName = blobClient.Name,
                Resource = "b", // b for blob
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(5),
            };

            sasBuilder.SetPermissions(BlobSasPermissions.Read);

            var storageAccountKey = await secretService.GetSecret("AzureBlobStorageKey");
            var sasToken = sasBuilder.ToSasQueryParameters(new StorageSharedKeyCredential(blobClient.AccountName, storageAccountKey)).ToString();

            return $"{blobClient.Uri.AbsoluteUri}?{sasToken}";
        }
    }
}

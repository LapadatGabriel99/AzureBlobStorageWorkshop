using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;

namespace Image.Api.Extensions
{
    public static class BlobClientExensions
    {
        public static string GetBlobUri(this BlobClient blobClient, IConfiguration configuration)
        {
            BlobSasBuilder sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = blobClient.BlobContainerName,
                BlobName = blobClient.Name,
                Resource = "b", // b for blob
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(1),
            };

            sasBuilder.SetPermissions(BlobSasPermissions.Read);

            var storageAccountKey = configuration.GetValue<string>("AzureBlobStorageKey");
            var sasToken = sasBuilder.ToSasQueryParameters(new StorageSharedKeyCredential(blobClient.AccountName, storageAccountKey)).ToString();

            return $"{blobClient.Uri.AbsoluteUri}?{sasToken}";
        }
    }
}

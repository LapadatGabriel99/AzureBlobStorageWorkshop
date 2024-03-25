using Image.Api.Models;

namespace Image.Api.Services.Contracts
{
    public interface IBlobService
    {
        Task<bool> UploadBlob(string containerName, string blobName, Stream fileStream, string contentType);

        Task<BlobFileResult> DownloadBlob(string containerName, string blobName);

        Task<string> RetrieveBlob(string containerName, string blobName);

        Task<bool> DeleteBlob(string containerName, string blobName);
    }
}

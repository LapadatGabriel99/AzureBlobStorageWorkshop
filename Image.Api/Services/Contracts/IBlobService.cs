namespace Image.Api.Services.Contracts
{
    public interface IBlobService
    {
        Task<bool> UploadBlob(string containerName, string blobName, Stream fileStream, string contentType);
    }
}

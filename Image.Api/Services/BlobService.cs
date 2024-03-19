using Azure.Storage.Blobs;
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
    }
}

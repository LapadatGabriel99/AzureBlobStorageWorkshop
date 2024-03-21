using Image.Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Image.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IBlobService _blobService;
        private readonly IConfiguration _configuration;

        public ImageController(ILogger<ImageController> logger, IBlobService blobService, IConfiguration configuration)
        {
            _logger = logger;
            _blobService = blobService;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("download")]
        public async Task<ActionResult> Download([FromQuery] string fileName)
        {
            var containerName = _configuration.GetValue<string>("AzureBlobStorageContainer");
            var blobFileResult = await _blobService.DownloadBlob(containerName, fileName);

            return File(blobFileResult.FileStream, blobFileResult.ContentType, blobFileResult.FileName);
        }

        [HttpGet]
        [Route("retrieve")]
        public async Task<ActionResult<string>> Retrieve([FromQuery] string fileName)
        {
            var containerName = _configuration.GetValue<string>("AzureBlobStorageContainer");
            var uri = await _blobService.RetrieveBlob(containerName, fileName);

            return Ok(uri);
        }

        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult> Upload(IFormFile formFile)
        {
            var containerName = _configuration.GetValue<string>("AzureBlobStorageContainer");
            var blobName = Guid.NewGuid() + Path.GetExtension(formFile.FileName);
            var fileStream = formFile.OpenReadStream();

            var result = await _blobService.UploadBlob(containerName, blobName, fileStream, formFile.ContentType);

            if (!result)
                return BadRequest($"Unable to upload file {formFile.FileName} to azure storage!");

            return Ok($"File with blob id {blobName} uploaded successfully!");
        }
    }
}

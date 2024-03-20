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

        public ImageController(ILogger<ImageController> logger, IBlobService blobService)
        {
            _logger = logger;
            _blobService = blobService;
        }

        [HttpGet]
        [Route("download")]
        public async Task<ActionResult<string>> Download([FromQuery] string fileName)
        {
            var blobFileResult = await _blobService.DownloadBlob("sample-container", fileName);

            return File(blobFileResult.FileStream, blobFileResult.ContentType, blobFileResult.FileName);
        }

        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult> Upload(IFormFile formFile)
        {
            var blobName = Guid.NewGuid() + Path.GetExtension(formFile.FileName);
            var fileStream = formFile.OpenReadStream();

            var result = await _blobService.UploadBlob("sample-container", blobName, fileStream, formFile.ContentType);

            if (!result)
                return BadRequest("Unable to upload file to azure storage!");

            return Ok("File uploaded successfully!");
        }
    }
}

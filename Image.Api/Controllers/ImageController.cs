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
            return Ok();
        }

        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult<string>> Upload(IFormFile formFile)
        {
            return Ok();
        }
    }
}

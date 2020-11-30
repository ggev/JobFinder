using JobFinder.Application.Enums;
using JobFinder.Application.Services.Files;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace JobFinder.Controllers
{
    public class FileController : BaseController
    {
        private readonly IFilesService _service;

        public FileController(IFilesService service)
        {
            _service = service;
        }

        [HttpGet("{type}/{fileName}")]
        public async Task<IActionResult> Get(UploadType type,  string fileName)
        {
            var result = await _service.Resize(type, fileName, 100, 100);
            if (result == null)
                return NotFound();
            var stream = new MemoryStream(result.Bytes);
            return File(stream, result.ContentType);
        }

    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Demelain.Server.Services;

namespace Demelain.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ILocalFileService _localFileService;

        public FileController(ILocalFileService localFileService)
        {
            _localFileService = localFileService;
        }

        // GET api/file/resume
        [HttpGet("resume")]
        [EnableCors]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Resume()
        {
            const string resumeName = "resume.pdf";
            
            var stream = await _localFileService.RetrieveLocalFileStream(resumeName);

            if (stream != null)
            {
                return File(stream, 
                    "application/octet-stream",
                    "Jon Karlsen - Resume.pdf");    
            }

            return NotFound();
        }
    }
}
using System.Threading.Tasks;
using Demelain.Server.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demelain.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalDetailsController : ControllerBase
    {
        private readonly IPersonalDetailsService _personalDetailsService;

        public PersonalDetailsController(IPersonalDetailsService personalDetailsService)
        {
            _personalDetailsService = personalDetailsService;
        }

        // GET: /api/personal-details
        [EnableCors]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
                return BadRequest("The 'id' parameter cannot be null. Please try again with a valid parameter.");

            var result =
                await _personalDetailsService
                    .GetByIdAsync(id.GetValueOrDefault());

            if (result != null)
                return Ok(result);

            return NotFound();
        }
    }
}
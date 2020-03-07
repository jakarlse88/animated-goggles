using Microsoft.AspNetCore.Mvc;
using Demelain.Shared.Models;
using Microsoft.AspNetCore.Authorization;

namespace Demelain.Server.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class ClientAppSettingsController : ControllerBase
    {
        private readonly ClientAppSettings _clientAppSettings;

        public ClientAppSettingsController(ClientAppSettings clientAppSettings)
        {
            _clientAppSettings = clientAppSettings;
        }

        [HttpGet]
        public IActionResult GetClientAppSettings()
        {
            return new JsonResult(_clientAppSettings);
        }
    }
}

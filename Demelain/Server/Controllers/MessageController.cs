using System;
using System.Threading.Tasks;
using Demelain.Server.Models;
using Demelain.Server.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demelain.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [EnableCors]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostMessage(ContactFormDto dto)
        {
            if (dto == null)
                return BadRequest("You tried to send a message without providing any data.");

            var success = await _messageService.SendMessage(dto);

            if (success)
                return Ok("Message successfully sent.");

            throw new Exception("There was an error sending the message.");
        }
    }
}
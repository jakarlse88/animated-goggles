using System;
using System.Threading.Tasks;
using Demelain.Server.ActionFilters;
using Demelain.Server.Models.InputModels;
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

        [HttpPost, EnableCors, ValidateModel]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult PostMessage(ContactFormInputModel model)
        {
            if (model == null)
            {
                return BadRequest("You tried to send a message without providing any data.");
            }

            _messageService.SendMessage(model);

            return NoContent();
        }
    }
}
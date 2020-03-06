using System.Net;
using System.Threading.Tasks;
using Demelain.AuthServer.Models;
using Demelain.AuthServer.Quickstart.Account;
using IdentityServer4.Events;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demelain.AuthServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;


        public AuthenticationController(IIdentityServerInteractionService interaction,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IEventService events)
        {
            _interaction = interaction;
            _userManager = userManager;
            _signInManager = signInManager;
            _events = events;
        }

        [HttpPost]
        [EnableCors]
        public async Task<IActionResult> Login([FromBody] LoginInputModel model)
        {
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            if (!ModelState.IsValid) return BadRequest();

            var result =
                await _signInManager.PasswordSignInAsync(model.Username, model.Password,
                    model.RememberLogin, true);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName,
                    clientId: context?.ClientId));

                if (user != null && context != null)
                {
                    await HttpContext.SignInAsync(user.Id, user.UserName);

                    return new JsonResult(new {RedirectUrl = model.ReturnUrl, IsOk = true});
                }
            }

            await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials",
                clientId: context?.ClientId));

            return Unauthorized();
        }
    }
}
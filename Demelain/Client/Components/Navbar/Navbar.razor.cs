using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sotsera.Blazor.Oidc;

namespace Demelain.Client.Components
{
    public class NavbarBase : ComponentBase
    {
        [Inject] private IJSRuntime JsRuntime { get; set; }
        [Inject] private IUserManager UserManager { get; set; }
        protected string NavbarHeading { get; }

        public NavbarBase()
        {
            NavbarHeading = "Jon Karlsen";
        }

        protected async void SignOutHandler() => await UserManager.BeginLogoutAsync(p => p.WithRedirect());

        protected async void SignInHandler() => await UserManager.BeginAuthenticationAsync(p => p.WithRedirect());

        protected async Task ScrollToSection(string sectionId)
        {
            await JsRuntime
                .InvokeVoidAsync(
                    "linkToPageSection",
                    sectionId);
        }
    }
}
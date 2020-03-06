using Microsoft.AspNetCore.Components;
using Sotsera.Blazor.Oidc;

namespace Demelain.Client.Components.AppAuthorization
{
    public class AppAuthorizationBase : ComponentBase
    {
        [Inject] public IUserManager UserManager { get; set; }
        
        public async void LoginPopup() => await UserManager.BeginAuthenticationAsync();
        public async void LoginRedirect() => await UserManager.BeginAuthenticationAsync(p => p.WithRedirect());
    
        public async void LogoutPopup() => await UserManager.BeginLogoutAsync();
        public async void LogoutRedirect() => await UserManager.BeginLogoutAsync(p => p.WithRedirect());
    }
}
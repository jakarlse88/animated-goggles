using System;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sotsera.Blazor.Oidc;
using Sotsera.Blazor.Oidc.Configuration.Model;
using Sotsera.Blazor.Toaster.Core.Models;

namespace Demelain.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddOidc(new Uri("http://localhost:5000"), (settings, siteUri) =>
                {
                    settings.UseDefaultCallbackUris(siteUri);
                    settings.UseRedirectToCallerAfterAuthenticationRedirect();
                    // settings.UseRedirectToCallerAfterLogoutRedirect();
                    settings.LogoutRedirectCallbackUri = "http://localhost:5002/";
                    
                    settings.ClientId = "demelain_client";
                    settings.ResponseType = "code";
                    
                    settings.Scope = "openid profile demelain_server";
                    
                    settings.MinimumLogeLevel = LogLevel.Information;
                    settings.StorageType = StorageType.SessionStorage;
                    settings.InteractionType = InteractionType.Popup;
            
                });

            builder.Services.AddAuthorizationCore(options => {});
            
            builder.Services.AddToaster(c => c.PositionClass = Defaults.Classes.Position.BottomRight);
            
            await builder.Build().RunAsync();
        }
    }
}

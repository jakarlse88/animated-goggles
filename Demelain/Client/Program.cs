using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Blazored.LocalStorage;
using Demelain.Client.Services;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Sotsera.Blazor.Oidc;
using Sotsera.Blazor.Oidc.Configuration.Model;

namespace Demelain.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBlazoredLocalStorage();

//            builder.Services.AddOidc(new Uri("https://demo.identityserver.io/"), (settings, siteUri) =>
//                {
//                    settings.UseDefaultCallbackUris(siteUri);
//                    settings.UseRedirectToCallerAfterAuthenticationRedirect();
//                    settings.UseRedirectToCallerAfterLogoutRedirect();
//
//                    settings.ClientId = "demelain_client";
//                    settings.ClientSecret = "secret";
//                    
//                    settings.ResponseType = "code";
//
//                    settings.StorageType = StorageType.LocalStorage;
//
//                    settings.Scope = "demelain_server offline";
//                });

            builder.Services.AddOidc(new Uri("https://demo.identityserver.io"), (settings, siteUri) =>
            {
                settings.UseDefaultCallbackUris(siteUri);
                settings.UseRedirectToCallerAfterAuthenticationRedirect();
                settings.UseRedirectToCallerAfterLogoutRedirect();
                settings.UseDemoFlow().Code(); // Just for this demo: allows to quickly change to one of the supported flows
                settings.Scope = "openid profile email api";

//                settings.MinimumLogeLevel = LogLevel.Information;
                settings.StorageType = StorageType.SessionStorage;
                settings.InteractionType = InteractionType.Popup;
            });

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
//            builder.Services.AddScoped<AuthenticationState Provider, DemelainAuthenticationStateProvider>();
            
            await builder.Build().RunAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Blazored.LocalStorage;
using Demelain.Client.Services;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
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

            builder.Services.AddOidc(new Uri("https://demo.identityserver.io"), (settings, siteUri) =>
            {
                settings.UseDefaultCallbackUris(siteUri);
                settings.UseRedirectToCallerAfterAuthenticationRedirect();
                settings.UseRedirectToCallerAfterLogoutRedirect();
                settings.UseDemoFlow().Code(); // Just for this demo: allows to quickly change to one of the supported flows
                settings.Scope = "openid profile email api";

                settings.MinimumLogeLevel = LogLevel.Information;
                settings.StorageType = StorageType.SessionStorage;
                settings.InteractionType = InteractionType.Popup;
            });

            // builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore(options => {});
            // builder.Services.AddBlazoredLocalStorage();
            // builder.Services.AddScoped<DemelainAuthenticationStateProvider>();
            // builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
            //     provider.GetRequiredService<DemelainAuthenticationStateProvider>());
            
            builder.Services.AddToaster(c => c.PositionClass = Defaults.Classes.Position.BottomRight);
            
            await builder.Build().RunAsync();
        }
    }
}

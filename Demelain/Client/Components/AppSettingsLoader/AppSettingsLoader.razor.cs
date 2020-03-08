using System;
using System.Net.Http;
using System.Threading.Tasks;
using Demelain.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Demelain.Client.Components.AppSettingsLoader
{
    /// <summary>
    /// Get ClientAppSettings from the server, and defer loading
    /// of the app until settings are loaded. 
    /// Settings are then propagated to the rest of the app as
    /// cascading values.
    /// Source: https://hutchcodes.net/2019/12/blazor-wasm-app-settings/
    /// </summary>
    public class AppSettingsLoaderBase : ComponentBase
    {
        [Inject] private HttpClient HttpClient { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected ClientAppSettings ClientAppSettings { get; set; }
        protected bool IsLoaded { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (!IsLoaded)
            {
                var appSettings = await HttpClient.GetJsonAsync<ClientAppSettings>("api/clientappsettings");

                ClientAppSettings = 
                    new ClientAppSettings
                    {
                        ClientId = appSettings.ClientId,
                        ClientSecret = appSettings.ClientSecret
                    };

                IsLoaded = true;
            }
        }
    }
}
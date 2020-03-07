using System;
using System.Net.Http;
using System.Threading.Tasks;
using Demelain.Shared.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Components;

namespace Demelain.Client.Components.About
{
    public class AboutBase : ComponentBase
    {
        [CascadingParameter] public ClientAppSettings ClientAppSettings { get; set; }
        [Inject] private HttpClient HttpClient { get; set; }
        protected AboutSectionData AboutSectionData { get; set; }

        // protected override async Task OnInitializedAsync()
        // {
        //     AboutSectionData = 
        //         await HttpClient.GetJsonAsync<AboutSectionData>("https://localhost:5003/api/personaldetails/1");
        // }

        protected async Task GetResumeFileFromServerAsync()
        {
            var discovery = await HttpClient.GetDiscoveryDocumentAsync("http://localhost:5001");

            if (discovery.IsError)
            {
                Console.WriteLine(discovery.Error);
                return;
            }

            var tokenResponse = await HttpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discovery.TokenEndpoint,
                    
                ClientId = "demelain_client",
                // TODO: Add configuration somehow, get secret
//                ClientSecret = "{BDE3A135-77DF-4D33-BB19-A9EC5B92A98D}",
                ClientSecret = "secret",
                Scope = "demelain_server"
            });

            if (tokenResponse.IsError)
            {
                System.Diagnostics.Debug.WriteLine(tokenResponse.Error);
                return;
            }

            HttpClient.SetBearerToken(tokenResponse.AccessToken);
            
            await HttpClient.GetAsync("api/file/resume");
        }
    }

    public class AboutSectionData
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string Biography { get; set; }
    }
}
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Demelain.Client.Models.InputModels;
using Demelain.Client.Models.ResultModels;
using Demelain.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Demelain.Client.Services;
using Sotsera.Blazor.Oidc;

namespace Demelain.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly OidcHttpClient _oidcHttpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorageService;

        public AuthService(OidcHttpClient oidcHttpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorageService)
        {
            _oidcHttpClient = oidcHttpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorageService = localStorageService;
        }

        /// <summary>
        /// Asynchronously registers a user against the auth server.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<RegisterResult> RegisterAsync(RegisterInputModel model)
        {
            var result =
                await _oidcHttpClient.PostJsonAsync<RegisterResult>("http://localhost:5000/account/register", model);

            return result;
        }

        /// <summary>
        /// Asynchronously logs a user in against the auth server.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<LoginResult> LoginAsync(LoginInputModel model)
        {
            var loginAsJson = JsonSerializer.Serialize(model);
           
            var response = 
                await _oidcHttpClient.PostAsync("http://localhost:5000/account/login", new StringContent(loginAsJson));

            var loginResult = JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync());

            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await _localStorageService.SetItemAsync("authToken", loginResult.Token);
            
            ((DemelainAuthenticationStateProvider) _authenticationStateProvider).MarkUserAsAuthenticated(model.Username);
            
            _oidcHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

            return loginResult;
        }

        /// <summary>
        /// Asynchronously logs a user out against the auth server.
        /// </summary>
        /// <returns></returns>
        public async Task LogoutAsync()
        {
            await _localStorageService.RemoveItemAsync("authToken");
            
            ((DemelainAuthenticationStateProvider) _authenticationStateProvider).MarkUserAsLoggedOut();

            _oidcHttpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Demelain.Client.Services
{
    // Source: https://chrissainty.com/securing-your-blazor-apps-authentication-with-clientside-blazor-using-webapi-aspnet-core-identity/
    /// <summary>
    /// This method is called by the CascadingAuthenticationState component to
    /// determine whether the current user is authenticated or not. 
    /// </summary>
    public class DemelainAuthenticationStateProvider : AuthenticationStateProvider
    {
        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private ILocalStorageService LocalStorageService { get; set; }

        /// <summary>
        /// This methods checks to see whether there is an auth token in local storage.
        /// If no, returns a new AuthenticationState with a blank claims principal (ie. user is not auth'd.)
        /// If yes, retrieve it and set default auth header for HttpClient. Then return a new
        /// AuthenticationState with a new claims principal containing the claims from the token. 
        /// </summary>
        /// <returns></returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await LocalStorageService.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return new AuthenticationState(
                    new ClaimsPrincipal(
                        new ClaimsIdentity()));
            }

            HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", savedToken);

            return new AuthenticationState(
                new ClaimsPrincipal(
                    new ClaimsIdentity(
                        ParseClaimsFromJwt(savedToken),
                        "jwt")));
        }

        /// <summary>
        /// Marks a user as authenticated.
        /// </summary>
        /// <param name="email"></param>
        public void MarkUserAuthenticated(string email)
        {
            var authenticatedUser =
                new ClaimsPrincipal(
                    new ClaimsIdentity(
                        new[] {new Claim(ClaimTypes.Name, email)}, "apiauth"));

            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            
            NotifyAuthenticationStateChanged(authState);
        }

        /// <summary>
        /// Marks a user as logged out.
        /// </summary>
        public void MarkUserLoggedOut()
        {
            var anonymousUser = 
                new ClaimsPrincipal(
                    new ClaimsIdentity());

            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            
            NotifyAuthenticationStateChanged(authState);
        }
        
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }

            return Convert.FromBase64String(base64);
        }
    }
}
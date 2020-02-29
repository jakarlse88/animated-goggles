using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace Demelain.AuthServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>();
        }

        public static IEnumerable<ApiResource> GetApiResourcesResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("demelain_server", "Demelain Server")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "Demelain/BlazorWASM",
                    ClientSecrets =
                    {
                        new Secret(Startup.StaticConfiguration["Clients:Demelain-WASM:Secret"].Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,

                    RedirectUris = {"http://localhost:5001/signin"},
                    PostLogoutRedirectUris = {"http://localhost:5001/signout-callback"},

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "demelain_server"
                    },

                    AllowOfflineAccess = true
                }
            };
        }
    }
}
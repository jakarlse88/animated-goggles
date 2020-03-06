// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace Demelain.AuthServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource>();


        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("demelain_server", "Demelain Web API Server")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "demelain_client",
                    ClientName = "Demelain Blazor/WASM Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    ClientSecrets =
                    {
//                        new Secret(Startup.StaticConfiguration["Clients:Demelain-WASM:Secret"].Sha256())
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = {"http://localhost:5001/signin"},
                    PostLogoutRedirectUris = {"http://localhost:5001/signout-callback"},

                    AllowedCorsOrigins = {"http://localhost:5000", "http://localhost:5002"},

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
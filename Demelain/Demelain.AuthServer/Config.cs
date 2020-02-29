using System.Collections.Generic;
using IdentityServer4.Models;

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
            return new List<ApiResource>();
        }
        
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>();
        }
    }
}
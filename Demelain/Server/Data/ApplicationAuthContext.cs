﻿using Demelain.Server.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

 namespace Demelain.Server.Data
{
    public class ApplicationAuthContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationAuthContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        } 
    }
}

﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demelain.AuthServer.Areas.Identity.Data
{
    public class DemelainAuthServerContext : IdentityDbContext<DemelainAuthServerUser>
    {
        public DemelainAuthServerContext(DbContextOptions<DemelainAuthServerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}

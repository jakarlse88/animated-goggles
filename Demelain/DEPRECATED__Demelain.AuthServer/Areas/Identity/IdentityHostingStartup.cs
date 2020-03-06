//using System;
//using Demelain.AuthServer.Areas.Identity.Data;
//using Demelain.AuthServer.Data;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//
//[assembly: HostingStartup(typeof(Demelain.AuthServer.Areas.Identity.IdentityHostingStartup))]
//namespace Demelain.AuthServer.Areas.Identity
//{
//    public class IdentityHostingStartup : IHostingStartup
//    {
//        public void Configure(IWebHostBuilder builder)
//        {
//            builder.ConfigureServices((context, services) => {
//                services.AddDbContext<DemelainAuthServerContext>(options =>
//                    options.UseSqlServer(
//                        context.Configuration.GetConnectionString("DemelainAuthServerContextConnection")));
//
//                services.AddDefaultIdentity<DemelainAuthServerUser>(options => options.SignIn.RequireConfirmedAccount = true)
//                    .AddEntityFrameworkStores<DemelainAuthServerContext>();
//            });
//        }
//    }
//}
using System.Linq;
using System.Reflection;
using Demelain.AuthServer.Areas.Identity.Data;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demelain.AuthServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            
            services.AddControllersWithViews();

            services.AddDbContext<DemelainAuthServerContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DemelainAuthServerContextConnection")));

            services.AddDefaultIdentity<DemelainAuthServerUser>(
                    options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DemelainAuthServerContext>()
                .AddDefaultTokenProviders();
            
            services.AddIdentityServer()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(
                            Configuration.GetConnectionString("DemelainAuthServerContextConnection"),
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                    };
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(
                            Configuration.GetConnectionString("DemelainAuthServerContextConnection"),
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                    };

                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                })
                .AddAspNetIdentity<DemelainAuthServerUser>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            InitialiseDatabase(app);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // This method initialises the operational/config database for IdentityServer4
        private void InitialiseDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<DemelainAuthServerContext>().Database.Migrate();
                
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                
                using (var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>())
                {
                    context.Database.Migrate();

                    if (!context.Clients.Any())
                    {
                        foreach (var client in Config.GetClients())
                        {
                            context.Clients.Add(client.ToEntity());
                        }

                        context.SaveChanges();
                    }

                    if (!context.IdentityResources.Any())
                    {
                        foreach (var resource in Config.GetIdentityResources())
                        {
                            context.IdentityResources.Add(resource.ToEntity());
                        }

                        context.SaveChanges();
                    }

                    if (!context.ApiResources.Any())
                    {
                        foreach (var resource in Config.GetApiResourcesResources())
                        {
                            context.ApiResources.Add(resource.ToEntity());
                        }

                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
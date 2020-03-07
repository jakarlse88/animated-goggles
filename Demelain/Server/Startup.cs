using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Text;
using Demelain.Server.Data;
using Demelain.Server.Repositories;
using Demelain.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Demelain.Shared.Models;
using Microsoft.IdentityModel.Tokens;

namespace Demelain.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            // Facilitate the passing of client app configuration from server 
            services.AddSingleton(Configuration.GetSection("ClientAppSettings").Get<ClientAppSettings>());
            
            services.AddDbContext<DemelainContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DemelainContextConnection")));

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            services.AddScoped<IPersonalDetailsService, PersonalDetailsService>();
            services.AddScoped<ILocalFileService, LocalFileService>();

            services.AddTransient<IMessageService, MessageService>();

            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v0.1.0", new OpenApiInfo { Title = "Nexus API", Version = "v0.1.0" })
            );

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5001";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "demelain_server";
                    
//                    options.TokenValidationParameters = new TokenValidationParameters
//                    {
//                        ValidateIssuer = true,
//                        ValidateAudience = true,
//                        ValidateLifetime = true,
//                        ValidateIssuerSigningKey = true,
//                        ValidIssuer = Configuration["JwtIssuer"],
//                        ValidAudience = Configuration["JwtAudience"],
//                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]))
//                    };
                    
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<DemelainContext>().Database.Migrate();
            }

            app.UseResponseCompression();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v0.1.0/swagger.json", "Nexus API v0.1.0"));
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();
            app.UseClientSideBlazorFiles<Client.Program>();

            app.UseRouting();

            app.UseCors(options =>
                options
                    .WithOrigins("http://localhost:5000",
                        "https://localhost:5001")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithHeaders(HeaderNames.AccessControlAllowOrigin, "*")
            );

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToClientSideBlazor<Client.Program>("index.html");
            });
        }
    }
}

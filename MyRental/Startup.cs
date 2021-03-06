using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyRental.Models;
using MyRental.Models.UserModel;
using MyRental.Services.ItemServices;
using MyRental.Services.MessageServices;

namespace MyRental
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
            services.AddDbContext<MyRentalDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();
            services.AddRazorPages();
            
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IMessageService, MessageService>();

            // user individual authentication
            services.AddDefaultIdentity<ApplicationUser>(options => {
            })
                .AddEntityFrameworkStores<MyRentalDbContext>();

            services.AddIdentityServer()
                //.AddSigningCredential(new X509Certificate2(Path.Combine(".", "certs", "IdentityServer4Auth.pfx")))
                //.AddInMemoryApiResources(MyApiResourceProvider.GetAllResources()) // <- THE NEW LINE 
                .AddApiAuthorization<ApplicationUser, MyRentalDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "clientapp/build";
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //SetupDB(env.IsDevelopment());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            // Comment this line for temporary deployment
            // This line will for raise an error if no https configured
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}");
                //endpoints.MapControllers();
                endpoints.MapRazorPages();
                
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "clientapp";
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

        // Setup Database connection string
        //private void SetupDB(bool isDev)
        //{
        //    if (isDev)
        //    {
        //        CustomSettings.ConnectionString = $"Server=(localdb)\\mssqllocaldb;Database=aspnet-myrental;Trusted_Connection=True;MultipleActiveResultSets=true";
        //            //$"Server={Configuration["DB:dev:server"]};Database={Configuration["DB:dev:database"]};user={Configuration["DB:dev:user"]};password={Configuration["DB:dev:password"]}";
        //    }
        //    else
        //    {
        //        //TODO: updata connectionstring for prod env
        //        CustomSettings.ConnectionString = $"Server={Configuration["DB:dev:server"]};Database={Configuration["DB:dev:database"]};user={Configuration["DB:dev:user"]};password={Configuration["DB:dev:password"]}";
        //    }
        //}
    }
}

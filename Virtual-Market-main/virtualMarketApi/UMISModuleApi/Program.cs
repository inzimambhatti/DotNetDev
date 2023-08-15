using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace UMISModuleApi
{   
    public class Program
    {
        
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        
        // builder.Services.AddIdentityServer()
        //     .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        // builder.Services.AddAuthentication()
        //     .AddIdentityServerJwt()
        //     .AddGoogle(googleOptions =>
        //     {
        //         googleOptions.ClientId = builder.onfiguration["Authentication:Google:ClientId"];
        //         googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
        //     });

    }
}

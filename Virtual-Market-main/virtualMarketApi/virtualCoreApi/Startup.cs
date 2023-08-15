using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using virtualCoreApi.Entities;
using Microsoft.OpenApi.Models;
// using Microsoft.AspNetCore.SignalR;
using virtualCoreApi.Controllers;


namespace virtualCoreApi
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
            // services.AddSignalR();
            services.AddControllers();
            services.Configure<conStr>(Configuration.GetSection("conStr"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "virtualCoreApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "virtualCoreApi v1"));
            }

            

            app.UseHttpsRedirection();
            // app.MapSignalR();
            app.UseRouting();
            // app.UseWebSockets();

            // app.Use(async (context, next) =>
            // {
            //     if (context.WebSockets.IsWebSocketRequest)
            //     {
            //         var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            //         await ListenForNotifications(webSocket);
            //     }
            //     else
            //     {
            //         await next();
            //     }
            // });
                        // app.UseEndpoints(routes =>
            // {
            //     routes.MapHub<EventHub>("/eventHub");
            // });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

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
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using UMISModuleAPI.Configuration;
using UMISModuleAPI.Services;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Identity;

namespace UMISModuleApi
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
            
            services.AddControllers();

            services.Configure<conStr>(Configuration.GetSection("conStr"));

            // services.AddDbContext<ApplicationDbContext>(options =>
            //     options.UseSqlServer(
            //         Configuration.GetConnectionString("DefaultConnection")));
            // services.AddDefaultIdentity<IdentityUser>(options =>
            //     options.SignIn.RequireConfirmedAccount = true)
            //         .AddEntityFrameworkStores<ApplicationDbContext>();
            // services.AddRazorPages();

            // configure strongly typed settings object
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;

                // IConfigurationSection googleAuthSection = Configuration.GetSection("Authentication:Google");

                // options.ClientId = googleAuthSection["Authentication:Google:1090218191057-68vuet556d994d8hq200sjkuvcdaalqm.apps.googleusercontent.com"];
                // options.ClientSecret = googleAuthSection["Authentication:Google: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"];
            })
            .AddCookie()
            .AddGoogle(GoogleDefaults.AuthenticationScheme,options =>
            {
                options.ClientId = Configuration["Authentication:Google:ClientId"];
                options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                options.ClaimActions.MapJsonKey("urn:google:picture","picture","url");
                options.Events.OnRedirectToAuthorizationEndpoint = context =>
                {
                    context.Response.Redirect(context.RedirectUri + "&prompt=consent");
                    return Task.CompletedTask;
                };
            });
            // services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
            // {
            // options.RemoteAuthenticationTimeout = TimeSpan.FromSeconds(10);
            // options.Events.OnRemoteFailure = RemoteAuthFail;
            // });
            
            // configure DI for application services
            services.AddScoped<IUserService, UserService>();

            // apply allowanonymus filter            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UMISModuleApi", Version = "v1" });
            });
        }

        // private void SetOpenIdConnectOptions(OpenIdConnectOptions options)
        // {
        //     options.ClientId = Configuration["OpenIdSettings:ClientId"];
        //     options.ClientSecret = Configuration["OpenIdSettings:ClientSecret"];
        //     options.Authority = Configuration["OpenIdSettings:Authority"];
        //     options.MetadataAddress = $"{Configuration["OpenIdSettings:Authority"]}/.well-known/openid-configuration";
        //     options.GetClaimsFromUserInfoEndpoint = true;
        //     options.SignInScheme = "Cookies";
        //     options.ResponseType = OpenIdConnectResponseType.IdToken;

        //     options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        //     {
        //         // This sets the value of User.Identity.Name to users AD username
        //         NameClaimType = IdentityClaimTypes.WindowsAccountName,
        //         RoleClaimType = IdentityClaimTypes.Role,
        //         AuthenticationType = "Cookies",
        //         ValidateIssuer = false
        //     };

        //     // Scopes needed by application
        //     options.Scope.Add("openid");
        //     options.Scope.Add("profile");
        //     options.Scope.Add("roles");
        // }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UMISModuleApi v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseMvc();
            // app.AddOpenIdConnect("Auth0", options =>
            // {
            //     options.CallbackPath = new PathString("/signin-auth0");
            // });

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

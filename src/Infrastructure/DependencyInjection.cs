using Codidact.Application.Common.Interfaces;
using Codidact.Infrastructure.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Codidact.Infrastructure
{
    /// <summary>
    /// Dependency Injection module for the infrastructure
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds all of the application services into the service collection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseNpgsql(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "cookie";
                options.DefaultSignInScheme = "cookie";
                options.DefaultChallengeScheme = "oidc";
            })
               .AddCookie("cookie")
               .AddOpenIdConnect("oidc", options =>
               {
                   options.Authority = "http://localhost:5000";
                   options.RequireHttpsMetadata = false; // dev only
                   options.ClientId = "codidact_client";
                   options.ClientSecret = "acf2ec6fb01a4b698ba240c2b10a0243";
                   options.ResponseType = "code";
                   options.ResponseMode = "form_post";
                   options.CallbackPath = "/signin-oidc";

                    // Enable PKCE (authorization code flow only)
                    options.UsePkce = true;
               });

            return services;
        }
    }
}

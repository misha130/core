using Codidact.Application.Common.Interfaces;
using Codidact.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Codidact.Infrastructure
{
    /// <summary>
    /// Dependency Injection module for the infrastructure
    /// </summary>
    public static class InfrastructureModule
    {
        /// <summary>
        /// Adds all of the application services into the service collection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((scope, options) =>
            {
                var context = scope.GetRequiredService<IActionContextAccessor>();
                string connectionString = "DefaultConnection";
                var actionContext = context.ActionContext;
                if (actionContext != null)
                {
                    var routeValues = actionContext.RouteData.Values;
                    if (routeValues.ContainsKey("community"))
                    {
                        connectionString = routeValues["community"].ToString();
                    }
                }
                options.UseNpgsql(
                   configuration.GetConnectionString(connectionString),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            return services;
        }
    }
}

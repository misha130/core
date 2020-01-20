using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using AutoMapper;

namespace Codidact.Application
{
    /// <summary>
    /// Dependency Injection module for the application
    /// </summary>
    public static class ApplicationModule
    {
        /// <summary>
        /// Adds all of the application services into the service collection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }

    }
}

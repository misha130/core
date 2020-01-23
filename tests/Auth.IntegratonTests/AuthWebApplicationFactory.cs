using Codidact.Application.Common.Interfaces;
using Codidact.Auth;
using Codidact.Domain.Entities;
using Codidact.Infrastructure.Application.Persistence;
using Codidact.Infrastructure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Auth.IntegratonTests
{
    public class AuthWebApplicationFactory<TStartup> : WebApplicationFactory<Startup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureServices(services =>
                {
                    // Remove the app's ApplicationDbContext registration.
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                            typeof(DbContextOptions<ApplicationDbContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    // Mock the ApplicationDbContext with the in memory db
                    // database for testing.
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });

                    services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

                    // Build the service provider.
                    var sp = services.BuildServiceProvider();

                    // Create a scope to obtain a reference to the database
                    // context (ApplicationDbContext).
                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<ApplicationDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<AuthWebApplicationFactory<TStartup>>>();
                    var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();

                    // Ensure the database is created.
                    context.Database.EnsureCreated();

                    try
                    {
                        // Seed user management for tests.
                        InitializeIdentitiesForTests(userManager);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                })
                .UseEnvironment("Test");
        }

        public static void InitializeIdentitiesForTests(UserManager<ApplicationUser> userManager)
        {
            var user = userManager.CreateAsync(new ApplicationUser
            {
                Email = "john.doe@.com",
                UserName = "johndoe",
                EmailConfirmed = true,
            }, "password");
            user.GetAwaiter().GetResult();
        }
    }
}

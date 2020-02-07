using Codidact.Infrastructure.Identity;
using Codidact.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Codidact.Auth.IntegrationTests
{
    /// <summary>
    /// This factory creates an .net Core server with the configuration provided
    /// for the purpose of testing against it
    /// </summary>
    public class AuthWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
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

                    // Build the service provider.
                    var sp = services.BuildServiceProvider();

                    // Create a scope to obtain a reference to the database
                    // context (ApplicationDbContext).
                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();

                    var logger = scopedServices.GetRequiredService<ILogger<AuthWebApplicationFactory<TStartup>>>();

                    try
                    {
                        // Seed the stores with test data.
                        InitializeStoresForTests(userManager).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                })
                .UseEnvironment("Test");
        }

        public static async Task InitializeStoresForTests(UserManager<ApplicationUser> userManager)
        {
            await userManager.CreateAsync(new ApplicationUser
            {
                UserName = "Admin",
                Email = "Admin@Codidact.com",
            }, "Aa123456!").ConfigureAwait(false);
        }
    }
}

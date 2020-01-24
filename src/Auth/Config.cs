using IdentityServer4.Models;
using System.Collections.Generic;

namespace Codidact.Auth
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("codidact", "Codidact API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "codidact_client",
                    ClientName = "Codidact Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    ClientSecrets = {new Secret("acf2ec6fb01a4b698ba240c2b10a0243".Sha256())},
                    RedirectUris = {"http://localhost:5001/signin-oidc", },
                    AllowedScopes = {"openid", "profile", "codidact"},
                    RequirePkce = true,
                    AllowPlainTextPkce = false
                }
            };
        }
    }
}
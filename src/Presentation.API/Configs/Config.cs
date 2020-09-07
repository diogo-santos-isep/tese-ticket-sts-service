namespace Presentation.API.Configs
{
    using IdentityServer4.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Config
    {
        internal static IEnumerable<Client> GetClients()
        {
            return new List<Client>(){
                new Client{
                    ClientId = "user_service",
                    ClientName = "User Management Service",
                    ClientSecrets = new List<Secret>{new Secret("secret".Sha512()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "configurations.service" },
                    AllowedCorsOrigins = {  },
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 3600
                },new Client{
                    ClientId = "ticket_application",
                    ClientSecrets = new List<Secret>{new Secret("secret".Sha512()) },
                    ClientName = "Back Office Ticket Application",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "configurations.service","user.service" },
                    RedirectUris = {"http://localhost:4200/auth-callback"},
                    PostLogoutRedirectUris = {"http://localhost:4200/"},
                    AllowedCorsOrigins = {"http://localhost:4200"},
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 3600
                }
            };
        }
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                // backward compat
                new ApiScope("configurations.service"),
                
                // more formal
                new ApiScope("user.service"),
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }
    }
}

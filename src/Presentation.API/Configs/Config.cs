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
                    ClientId = "ticket-info-service",
                    ClientName = "Ticket Management Service",
                    ClientSecrets = new List<Secret>{new Secret("secret".Sha512()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "configuration", "user", "department" },
                    AllowedCorsOrigins = {  },
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 3600
                },new Client{
                    ClientId = "ticket_application",
                    RequireClientSecret = false,
                    ClientSecrets = new List<Secret>{new Secret("secret".Sha512()) },
                    ClientName = "Back Office Ticket Application",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "configuration","user","department", "ticket", "ticket.list", "ticket.notes", "ticket.history" },
                    RedirectUris = {"http://localhost:4200/auth-callback"},
                    PostLogoutRedirectUris = {"http://localhost:4200/"},
                    AllowedCorsOrigins = {"http://localhost:4200"},
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 3600
                },new Client{
                    ClientId = "client_ticket_application",
                    RequireClientSecret = false,
                    ClientSecrets = new List<Secret>{new Secret("secret".Sha512()) },
                    ClientName = "Back Office Ticket Application",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "department","configuration","ticket.client", "ticket.create"},
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
                new ApiScope("configuration"),
                new ApiScope("user"),
                new ApiScope("ticket.client"),
                new ApiScope("ticket.notes"),
                new ApiScope("ticket.create"),
                new ApiScope("ticket.history"),
                new ApiScope("ticket"),
                new ApiScope("ticket.list"),
                new ApiScope("department"),
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

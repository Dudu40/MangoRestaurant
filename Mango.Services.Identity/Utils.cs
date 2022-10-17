using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using static System.Net.WebRequestMethods;

namespace Mango.Services.Identity
{
    public class Utils
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiScope> GetApiScope()
        {
            return new List<ApiScope>
            {
                new ApiScope("Mango", "MangoServer"),
                new ApiScope(name : "read", displayName : "Read your data."),
                new ApiScope(name : "write", displayName : "Write your data."),
                new ApiScope(name : "delete", displayName : "Delete your data.")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId= "Client",
                    ClientSecrets= {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"read", "write"}
                },
                new Client
                {
                    ClientId= "mango",
                    ClientSecrets= {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:7034/signin-oidc","https://localhost:7167/signin-oidc", "https://localhost:7034/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:7034/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "mango"
                    }
                }
            };
        }
    }
}

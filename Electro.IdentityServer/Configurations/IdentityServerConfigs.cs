using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace Electro.IdentityServer.Configurations
{
    public class IdentityServerConfigs
    {
        public const string Admin = "admin";
        public const string Customer = "customer";


        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };
        public static IEnumerable<ApiScope> ApiScopes =>

            new List<ApiScope>
            {
                new ApiScope(name: "all", "All permissions"),
                new ApiScope(name: "read",   displayName: "Read your data."),
                new ApiScope(name: "write",  displayName: "Write your data."),
                new ApiScope(name: "delete", displayName: "Delete your data.")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("ElectroApi")
                {
                    Scopes = new List<string>{ "all", "read", "write", "delete"},
                    ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256()) },
                    UserClaims = new List<string> {"role"}
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "Electro",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "api1", "api2.read_only" }
                },
                new Client
                {
                    ClientId = "ElectroMvcApp",
                    ClientSecrets = { new Secret("5UOY6F46W4".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = { "all",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        JwtClaimTypes.Role
                    },
                    RedirectUris={ "https://localhost:5001/signin-oidc" },
                    PostLogoutRedirectUris={"https://localhost:5001/signout-callback-oidc" },
                }
            };
    }
}

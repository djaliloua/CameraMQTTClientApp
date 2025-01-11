using Duende.IdentityServer.Models;

namespace IdentityServerApp
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            [
                new ApiScope("mqttapiscope"),
             new ApiScope("mqttconfigapiscope"),
            ];

        public static IEnumerable<Client> Clients =>
            [
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("4C1A7E1-0C79-4A89-A3D6-A37998FB80".Sha256(), new DateTime(2025, 12, 31)) },
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "mqttapiscope" }
                },
            new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("6A9B4E7B-2978-4CFD-B822-85C7B563059A".Sha256(), new DateTime(2025, 12, 31)) },
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "mqttconfigapiscope" }
                },
            ];
    }
}

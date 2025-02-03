using IdentityModel.Client;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Abstractions.Serialization;

namespace MQTTConfigApiConsume
{
   
    public class MyAccessTokenProvider : IAccessTokenProvider
    {
        public AllowedHostsValidator AllowedHostsValidator { get; } = new AllowedHostsValidator();
        public async Task<string> GetAuthorizationTokenAsync(Uri uri, Dictionary<string, object> additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
        {
            // Logic to fetch an access token (e.g., via MSAL or another method)
            return await GetAccessToken();
        }
        public async Task<string> GetAccessToken()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return disco.Error!;
            }
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0",
                Scope = "mqttapiscope"
            });
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return tokenResponse.Error!;
            }
            return tokenResponse.AccessToken!;
        }
    }
}

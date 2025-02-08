using Microsoft.Datasync.Client;
using Microsoft.Identity.Client;
using System.Diagnostics;

namespace TokenService
{
    public interface ITokenService
    {
        Task<AuthenticationToken> GetAuthenticationToken();
    }
    public class TokenService: ITokenService
    {
        private static IPublicClientApplication _identityClient;
        public async Task<AuthenticationToken> GetAuthenticationToken()
        {
            if (_identityClient == null)
            {
                if (OperatingSystem.IsAndroid())
                    _identityClient = PublicClientApplicationBuilder.Create(Constants.ApplicationId)
                        .WithAuthority(AzureCloudInstance.AzurePublic, Constants.TenantId)
                        .WithRedirectUri("msal96e4803a-1953-402d-8083-4cfc9a85b29d://auth")
                        .Build();
                else
                    _identityClient = PublicClientApplicationBuilder.Create(Constants.ApplicationId)
                        .WithAuthority(AzureCloudInstance.AzurePublic, Constants.TenantId)
                        .WithRedirectUri("http://localhost:62215")
                        .Build();
            }
            var accounts = await _identityClient.GetAccountsAsync();
            AuthenticationResult result = null;
            try
            {
                result = await _identityClient
                    .AcquireTokenSilent(Constants.Scopes, accounts.FirstOrDefault())
                    .ExecuteAsync();
            }
            catch (MsalUiRequiredException)
            {
                result = await _identityClient
                    .AcquireTokenInteractive(Constants.Scopes)
                    .ExecuteAsync();
            }
            catch (Exception ex)
            {
                // Display the error text - probably as a pop-up
                Debug.WriteLine($"Error: Authentication failed: {ex.Message}");
            }

            return new AuthenticationToken
            {
                DisplayName = result?.Account?.Username ?? "",
                ExpiresOn = result?.ExpiresOn ?? DateTimeOffset.MinValue,
                Token = result?.AccessToken ?? "",
                UserId = result?.Account?.Username ?? ""
            };
        }
    }
}

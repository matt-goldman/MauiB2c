using Microsoft.Identity.Client;

namespace MauiClient.Services
{
    public class AuthService
    {
        private IPublicClientApplication _publicClient;

        public AuthService()
        {
            _publicClient = PublicClientApplicationBuilder.Create(Constants.B2cClientId)
                .WithRedirectUri("goldieb2c://auth")
                .WithIosKeychainSecurityGroup("com.goldieb2c.mauiclient")
                .Build();
        }
    }
}

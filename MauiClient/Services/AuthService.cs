using Microsoft.Identity.Client;

namespace MauiClient.Services;

public class AuthService : IAuthService
{
    private IPublicClientApplication _publicClient;

    private bool _isAuthenticated = false;

    private string _userName;

    public AuthService()
    {
        _publicClient = PublicClientApplicationBuilder.Create(Constants.B2cClientId)
            .WithB2CAuthority(Constants.AuthorityUri)
            .WithRedirectUri("goldieb2cclient://auth")
            .WithTenantId(Constants.B2cTenantId)
            .WithIosKeychainSecurityGroup("com.goldieb2c.mauiclient")
            .Build();
    }

    public async Task SignInAsync()
    {
        try
        {
            var signInBuilder = _publicClient.AcquireTokenInteractive(Constants.B2cScopes)
                    .WithParentActivityOrWindow(App.ParentWindow);

            if (DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.iOS)
            {
                SystemWebViewOptions systemWebViewOptions = new SystemWebViewOptions
                {
                    iOSHidePrivacyPrompt = true
                };

                signInBuilder.WithUseEmbeddedWebView(false);
                signInBuilder.WithSystemWebViewOptions(systemWebViewOptions);
            }
            else
            {
                // macOS and Windows don't have a 'system browser'
                signInBuilder.WithUseEmbeddedWebView(true);
            }

            var result = await signInBuilder.ExecuteAsync();

            SetUserState(result);

        }
        catch (Exception ex)
        {

            await App.Current.MainPage.DisplayAlert("Acquire token interactive failed. See exception message for details: ", ex.Message, "Ok");
        }
    }

    public async Task RefreshLoginAsync()
    {
        try
        {
            var accounts = await _publicClient.GetAccountsAsync();
            var result = await _publicClient.AcquireTokenSilent(Constants.B2cScopes, accounts.FirstOrDefault())
                .ExecuteAsync();

            SetUserState(result);
        }
        catch (MsalUiRequiredException)
        {

            await SignInAsync();
        }
    }

    public async Task SignOutAsync()
    {
        var accounts = await _publicClient.GetAccountsAsync();

        while (accounts.Any())
        {
            await _publicClient.RemoveAsync(accounts.FirstOrDefault()).ConfigureAwait(false);
            accounts = await _publicClient.GetAccountsAsync().ConfigureAwait(false);
        }

        SetUserState(null);
    }

    private void SetUserState(AuthenticationResult? authenticationResult)
    {
        if (authenticationResult is not null)
        {
            Constants.AccessToken = authenticationResult.AccessToken;

            _isAuthenticated = true;

            _userName = authenticationResult.ClaimsPrincipal.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
        }
        else
        {
            _userName = null;

            _isAuthenticated = false;
        }
    }

    public bool IsAuthenticated { get =>_isAuthenticated; }

    public string UserName { get => _userName; }
}

namespace MauiClient.Services;

public interface IAuthService
{
    Task SignInAsync();

    Task SignOutAsync();

    Task RefreshLoginAsync();

    bool IsAuthenticated { get; }

    string UserName { get; }
}

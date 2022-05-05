using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiClient.Services;

namespace MauiClient.ViewModels;

[INotifyPropertyChanged]
public partial class MainViewModel
{
    private readonly IAuthService _auth;

    [ObservableProperty]
    string userName;

    [ObservableProperty]
    bool isSignedIn;

    public MainViewModel(IAuthService auth)
    {
        _auth = auth;
    }

    [ICommand]
    private async Task SignIn()
    {
        await _auth.SignInAsync();

        IsSignedIn = _auth.IsAuthenticated;

        UserName = _auth.UserName;
    }
}

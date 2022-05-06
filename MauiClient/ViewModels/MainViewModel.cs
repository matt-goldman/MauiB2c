using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiClient.Services;
using Shared;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace MauiClient.ViewModels;

[INotifyPropertyChanged]
public partial class MainViewModel
{
    private readonly IAuthService _auth;

    private readonly HttpClient _httpClient;

    public ObservableCollection<WeatherForecast> Forecasts { get; set; } = new();

    [ObservableProperty]
    string userName;

    [ObservableProperty]
    bool isSignedIn;

    [ObservableProperty]
    bool isLoading;

    public MainViewModel(IAuthService auth, IHttpClientFactory clientFactory)
    {
        _auth = auth;

        _httpClient = clientFactory.CreateClient(Constants.HttpClientName);
    }

    [ICommand]
    private async Task SignIn()
    {
        IsLoading = true;

        await _auth.SignInAsync();

        IsSignedIn = _auth.IsAuthenticated;

        UserName = _auth.UserName;

        IsLoading = false;
    }

    [ICommand]
    private async Task SignOut()
    {
        IsLoading = true;

        await _auth.SignOutAsync();

        IsSignedIn = _auth.IsAuthenticated;

        UserName = _auth.UserName;

        Forecasts.Clear();

        IsLoading = false;
    }

    [ICommand]
    private async Task FetchData()
    {
        IsLoading = true;

        var weatherData = await _httpClient.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");

        Forecasts.Clear();

        foreach(var forecast in weatherData)
        {
            Forecasts.Add(forecast);
        }

        IsLoading = false;
    }
}

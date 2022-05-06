using CommunityToolkit.Maui;
using MauiClient.Helpers;
using MauiClient.Services;
using MauiClient.ViewModels;

namespace MauiClient;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<IAuthService, AuthService>();
		builder.Services.AddSingleton<AuthHandler>();

		builder.Services.AddHttpClient(Constants.HttpClientName, client => 
		{ 
			client.BaseAddress = new Uri("https://6e95-159-196-124-207.ngrok.io");
		})
			.AddHttpMessageHandler((s) => s.GetService<AuthHandler>());


		builder.Services.AddTransient<MainPage>();

		builder.Services.AddTransient<MainViewModel>();

		return builder.Build();
	}
}

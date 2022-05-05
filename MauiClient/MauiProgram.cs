using CommunityToolkit.Maui;
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

		builder.Services.AddTransient<MainPage>();

		builder.Services.AddTransient<MainViewModel>();

		return builder.Build();
	}
}

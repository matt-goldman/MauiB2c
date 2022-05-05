namespace MauiClient;

public partial class App : Application
{
    public static object ParentWindow { get; set; }

    public App(MainPage mainPage)
	{
		InitializeComponent();

		MainPage = mainPage;
	}
}

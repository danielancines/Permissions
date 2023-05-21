using Maui.PermissionsProject.Permissions;
using Maui.PermissionsProject.Primitives;
using Microsoft.Extensions.Logging;

namespace Maui.PermissionsProject;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MainPageViewModel>();
		builder.Services.AddSingleton<IConfiguration, AppConfiguration>();
        builder.Services.AddSingleton<IPermissionConfigurator, PermissionConfigurator>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

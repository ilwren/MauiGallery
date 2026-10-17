using Microsoft.Extensions.Logging;

namespace MauiGallery;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif

#if ANDROID
		// Shell's flyout uses AndroidX DrawerLayout, which throws when measured
		// with anything but MeasureSpec.EXACTLY. Swap in a measure-safe renderer.
		builder.ConfigureMauiHandlers(handlers =>
			handlers.AddHandler<Microsoft.Maui.Controls.Shell,
				MauiGallery.Platforms.Android.FixedShellRenderer>());
#endif

		// Register the single routed page that hosts every control sample page.
		builder.Services.AddTransient<ControlInfoPage>();

		return builder.Build();
	}
}

using Armory;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using static DryIoc.Setup;

namespace Armory2
{
    internal static class PrismStartup
    {
        public static void Configure(PrismAppBuilder builder)
        {
            builder.RegisterTypes(RegisterTypes).CreateWindow(navigationService =>
            {
                var builder = navigationService.CreateBuilder();

                builder.AddSegment("MainPage");

                var builderNavigate = builder.NavigateAsync();

                return builderNavigate;
            });
        }

        private static void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }
    }

    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UsePrism(PrismStartup.Configure)
                .ConfigureMauiHandlers((handlers) =>
                {
#if IOS
                    handlers.AddHandler(typeof(CustomListViewRenderer), typeof(CustomListViewRenderer));
#endif
                }).ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();

            return builder.Build();
        }
    }
}


using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.Controls.Handlers.Compatibility;

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
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureMauiHandlers((handlers) =>
                {
#if IOS
                    handlers.AddHandler(typeof(ListView),typeof(ListViewHandler));
#endif
                });
//                .ConfigureEffects(effects =>
//                {
//#if IOS
//                    effects.Add<OnAttachedListenerRoutingEffect, OnAttachedListenerEffect>();
//#endif
//                })

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();

            return builder.Build();
        }
    }
}

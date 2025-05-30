using Microsoft.Extensions.Logging;

namespace PetProject
{
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
                    fonts.AddFont("Roboto-Black.ttf", "Roboto-Black");
                    fonts.AddFont("Roboto-Bold.ttf", "Roboto-Bold");
                    fonts.AddFont("Roboto-Regular.ttf", "Roboto-Regular");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

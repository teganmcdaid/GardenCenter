using GardenCenter.Services;
using GardenCenter.ViewModels;
using Microsoft.Extensions.Logging;

namespace GardenCenter
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
                });

            builder.Services.AddSingleton<ProductService>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<SignUpViewModel>();
            builder.Services.AddTransient<ProductsViewModel>();
            builder.Services.AddTransient<BasketViewModel>();
            builder.Services.AddTransient<CorporateViewModel>();



            return builder.Build();
        }
    }
}

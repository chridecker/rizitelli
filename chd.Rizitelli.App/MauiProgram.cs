using Blazored.Modal;
using chd.Rizitelli.App.Data;
#if ANDROID
using chd.Rizitelli.App.Platforms.Android;
using Maui.Android.InAppUpdates;

#elif IOS
using chd.Rizitelli.App.Platforms.iOS;
using UIKit;
#endif
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using SQLitePCL;
using chd.UI.Base.Contracts.Interfaces.Services;
using chd.UI.Base.Contracts.Interfaces.Update;
using chd.UI.Base.Client.Extensions;
using chd.UI.Base.Client.Implementations.Services;
using chd.Rizitelli.App.Services;
using Blazored.Modal.Services;

namespace chd.Rizitelli.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Batteries.Init();

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                 .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Configuration.AddConfiguration(GetLocalSetting());
            builder.Configuration.AddConfiguration(GetAppSettingsConfig());
            builder.Configuration.AddConfiguration(LoadLogoToBase64());

            builder.AddServices();
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();

            #if ANDROID
            builder.UseAndroidInAppUpdates(options =>
            {
                options.ImmediateUpdatePriority = 6;
            });
#endif

            var app = builder.Build();
            builder.InitDatabase();
            return app;
        }

        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
#if ANDROID
            services.AddAndroidServices();
#elif IOS
            services.AddiOS();
#endif
            services.AddUtilities<chdProfileService, int, int, UserIdLogInService, SettingManager, ISettingManager, UIComponentHandler, IBaseUIComponentHandler, MauiUpdateService>(ServiceLifetime.Singleton);
            services.AddMauiModalHandler();
            services.AddScoped<INavigationHistoryStateContainer, NavigationHistoryStateContainer>();
            services.AddScoped<INavigationHandler, NavigationHandler>();
            services.AddDataAccess(configuration);

             services.AddSingleton<IAppInfoService, AppInfoService>();

            services.AddSingleton<VibrationHelper>();

            services.AddSingleton<IDeviceInfo>(_ => DeviceInfo.Current);
            services.AddSingleton<IAppInfo>(_ => AppInfo.Current);

             services.RemoveAll<IModalService>();
            services.AddSingleton<ModalHandler>();
            services.AddSingleton<IModalService>(sp => sp.GetRequiredService<ModalHandler>());
            services.AddSingleton<IModalHandler>(sp => sp.GetRequiredService<ModalHandler>());
            services.AddSingleton<INavigationHistoryStateContainer, NavigationHistoryStateContainer>();
            services.AddScoped<INavigationHandler, NavigationHandler>();


            return services;
        }

        private static void InitDatabase(this MauiAppBuilder builder)
        {
#if ANDROID
            using var scope = builder.Services.BuildServiceProvider().CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataContext>();
            db.Database.EnsureCreated();
#elif IOS
            try
            {
                using var scope = builder.Services.BuildServiceProvider().CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<DataContext>();
                db.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
            }
#endif
        }

        private static void AddServices(this MauiAppBuilder builder)
        {
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddBlazoredModal();
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Services.AddAppServices(builder.Configuration);
        }
      
        private static IConfiguration LoadLogoToBase64()
        {
            var fileName = Path.Combine("wwwroot/logo.png");
            if (!FileSystem.AppPackageFileExistsAsync(fileName).Result)
            {
                throw new ApplicationException($"Unable to read file [{fileName}]");
            }
            using var mem = new MemoryStream();
            using var stream = FileSystem.OpenAppPackageFileAsync(fileName).Result;
            stream.CopyTo(mem);
            var base64 = Convert.ToBase64String(mem.ToArray());

            var dict = new Dictionary<string, string>();
            dict.Add($"Logo:Base64", $"{base64}");
            return new ConfigurationBuilder()
                    .AddInMemoryCollection(dict)
                    .Build();
        }


        private static IConfiguration GetAppSettingsConfig()
        {
            var fileName = "appsettings.txt";
            if (!FileSystem.AppPackageFileExistsAsync(fileName).Result)
            {
                throw new ApplicationException($"Unable to read file [{fileName}]");
            }
            using var stream = FileSystem.OpenAppPackageFileAsync(fileName).Result;
            return new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();
        }

        private static IConfiguration GetLocalSetting()
        {
            var path = Path.Combine(FileSystem.AppDataDirectory, DataContext.DB_FILE);

            var dict = new Dictionary<string, string>();

            dict.Add($"ConnectionStrings:ScoringContext", $"Data Source={path}");

            return new ConfigurationBuilder().AddInMemoryCollection(dict).Build();
        }
    }
}

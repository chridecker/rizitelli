using Blazored.Modal;
using chd.Rizitelli.App.Data;
#if ANDROID
using Maui.Android.InAppUpdates;
#elif IOS
#endif
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using SQLitePCL;
using chd.Rizitelli.Persistence.Data;

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

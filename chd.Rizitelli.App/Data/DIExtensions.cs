using Blazored.Modal.Services;
#if ANDROID
using chd.Rizitelli.App.Platforms.Android;
#elif IOS
using chd.Rizitelli.App.Platforms.iOS;
#endif
using chd.Rizitelli.App.Services;
using chd.Rizitelli.Contracts.Interfaces;
using chd.Rizitelli.Persistence.Data;
using chd.UI.Base.Client.Extensions;
using chd.UI.Base.Client.Implementations.Services;
using chd.UI.Base.Contracts.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Rizitelli.App.Data
{
    public static class DIExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
#if ANDROID
            services.AddAndroidServices();
            services.AddUtilities<chdProfileService, int, int, UserIdLogInService, AndroidSettingManager, ISettingManager, UIComponentHandler, IBaseUIComponentHandler, MauiUpdateService>(ServiceLifetime.Singleton);

#elif IOS
            services.AddUtilities<chdProfileService, int, int, UserIdLogInService, iOSSettingManager, ISettingManager, UIComponentHandler, IBaseUIComponentHandler, MauiUpdateService>(ServiceLifetime.Singleton);

            services.AddiOS();
#endif
            services.AddMauiModalHandler();
            services.AddScoped<INavigationHistoryStateContainer, NavigationHistoryStateContainer>();
            services.AddScoped<INavigationHandler, NavigationHandler>();
            services.AddDataAccess(configuration);

             services.AddSingleton<IAppInfoService, AppInfoService>();

            services.AddSingleton<IVibrationHelper,VibrationHelper>();

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
    }
}

using chd.Rizitelli.App.Services;
using chd.UI.Base.Contracts.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Rizitelli.App.Platforms.Android
{
    public class AndroidSettingManager : SettingManager
    {
        public AndroidSettingManager(ILogger<AndroidSettingManager> logger, IProtecedLocalStorageHandler protecedLocalStorageHandler, NavigationManager navigationManager) : base(logger, protecedLocalStorageHandler, navigationManager)
        {
        }

        protected override bool _isiOS()=> false;
    }
}

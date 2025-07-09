using chd.Rizitelli.Contracts.Interfaces;
using chd.UI.Base.Client.Implementations.Services.Base;
using chd.UI.Base.Contracts.Interfaces.Services;
using chd.UI.Base.Contracts.Interfaces.Services.Base;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Rizitelli.App.Services
{
    public abstract class SettingManager : BaseClientSettingManager<int, int>, ISettingManager
    {
        public SettingManager(ILogger<SettingManager> logger, IProtecedLocalStorageHandler protecedLocalStorageHandler,
            NavigationManager navigationManager) : base(logger, protecedLocalStorageHandler, navigationManager)
        {
        }
        public bool IsiOS => this._isiOS();

        protected abstract bool _isiOS();


    }

}

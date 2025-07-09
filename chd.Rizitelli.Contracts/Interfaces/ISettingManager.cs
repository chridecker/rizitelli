using chd.UI.Base.Contracts.Interfaces.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Rizitelli.Contracts.Interfaces
{
    public interface ISettingManager : IBaseClientSettingManager
    {
        bool IsiOS{get;}
    }
}

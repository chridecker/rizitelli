using chd.Rizitelli.App.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Rizitelli.App.Dtos
{
    public class Player : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}

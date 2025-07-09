using chd.Rizitelli.Contracts.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Rizitelli.Contracts.Dtos
{
    public class Game : BaseEntity<Guid>
    {
        public int Cards { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<PlayerGame> Players { get; set; }
    }
}

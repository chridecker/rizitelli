using chd.Rizitelli.Contracts.Dtos.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Rizitelli.Contracts.Dtos
{
    public class PlayerGame : BaseEntity<Guid>
    {
        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; set; }

        [ForeignKey(nameof(Game))]
        public Guid GameId { get; set; }
        public int SequenceNumber { get; set; }

         public virtual Player Player { get; set; }
        public virtual Game Game{ get; set; }
    }
}

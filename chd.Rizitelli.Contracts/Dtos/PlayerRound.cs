using chd.Rizitelli.Contracts.Dtos.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Rizitelli.Contracts.Dtos
{
    public class PlayerRound : BaseEntity<Guid>
    {
        [ForeignKey(nameof(Round))]
        public Guid RoundId { get; set; }
        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; set; }
        public int? PlannedTricks { get; set; }
        public int? Tricks { get; set; }

        public virtual Player Player { get; set; }
        public virtual Round Round{ get; set; }
    }
}

using chd.Rizitelli.Contracts.Dtos.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Rizitelli.Contracts.Dtos
{
    public class Round : BaseEntity<Guid>
    {
        [ForeignKey(nameof(Game))]
        public Guid GameId { get; set; }
        public int SequenceNumber { get; set; }
        public int StartPlayerId { get; set; }
        public int DialPlayerId { get; set; }
        public int Cards { get; set; }

        public virtual Game Game { get; set; }
        public virtual ICollection<PlayerRound> Players { get; set; }
    }
}

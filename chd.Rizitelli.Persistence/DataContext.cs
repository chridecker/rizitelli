using chd.Rizitelli.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Rizitelli.Persistence.Data
{
    public class DataContext : DbContext
    {
        public const string DB_FILE = "data.db";

        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games{ get; set; }
        public DbSet<PlayerGame> PlayerGames{ get; set; }
        public DbSet<PlayerRound> PlayerRounds{ get; set; }
        public DbSet<Round> Round{ get; set; }

        public DataContext() : base()
        {

        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.Migrate();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Filename={DB_FILE}");
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}

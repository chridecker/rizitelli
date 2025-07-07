using chd.Rizitelli.App.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Rizitelli.App.Data
{
    public class DataContext : DbContext
    {
        public const string DB_FILE = "data.db";

        public DbSet<Player> Players { get; set; }

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

using FlamingoProduction.Models;
using Microsoft.EntityFrameworkCore;

namespace FlamingoProduction.Database
{
    public class FlamingoContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=(localdb)\\mssqllocaldb;Database=Flamingo-Test;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        public virtual DbSet<LocalUser> LocalUsers { get; set; }
    }
}

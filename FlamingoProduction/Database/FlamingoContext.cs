using Flamingo.Fishes.Advanced.Attributes;
using FlamingoProduction.Models;
using Microsoft.EntityFrameworkCore;

namespace FlamingoProduction.Database
{
    public class FlamingoContext: DbContext
    {
        public MagicalItem MagicalItem { get; }

        [AdvancedHandlerConstructor]
        public FlamingoContext(MagicalItem magicalItem)
        {
            MagicalItem = magicalItem;
        }

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

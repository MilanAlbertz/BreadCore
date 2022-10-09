using Microsoft.EntityFrameworkCore;
using BreadCore.Models;

namespace BreadCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> contextoptions) : base(contextoptions)
        {

        }
        public DbSet<Brood> Brood { get; set; }
        public DbSet<Bakprogramma> Bakprogramma { get; set; }
        public DbSet<Medewerker> Medewerker { get; set; }
        public DbSet<Filiaal> Filiaal { get; set; }
        public DbSet<BroodType> BroodType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Medewerker>().HasData(new Medewerker
            { Id = 1, BedienerNr = 1, Wachtwoord = 1, Rol = "Systeem Beheerder" });
        }
    }
}

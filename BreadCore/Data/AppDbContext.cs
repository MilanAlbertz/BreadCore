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
        public DbSet<BreadCore.Models.Medewerker>? Medewerker { get; set; }
    }
}

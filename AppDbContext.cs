using Microsoft.EntityFrameworkCore;
using BreadCore.Models

namespace BreadCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(dBcontextOptions<AppDbContext> contextoptions) : base(contextoptions)
        {
        }

        public DbSet<Order> Orders { get; set; }
    }
}

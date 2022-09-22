﻿using Microsoft.EntityFrameworkCore;
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
    }
}

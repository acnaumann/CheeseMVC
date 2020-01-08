using System;
using CheeseMVC.Models;
using Microsoft.EntityFrameworkCore;
    

namespace CheeseMVC.Data
{
    public class CheeseDbContext : DbContext
    {
        public DbSet<Cheese> Cheeses { get; set; }
        //DbSet part of the Entity framework

        //public CheeseDbContext(DbContextOptions<CheeseDbContext> options) : base(options)
        //{
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=CheeseMVC.db");
        // => optionsBuilder??
    }
}

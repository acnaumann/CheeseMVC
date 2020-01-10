using System;
using CheeseMVC.Models;
using Microsoft.EntityFrameworkCore;
    

namespace CheeseMVC.Data
{
    public class CheeseDbContext : DbContext
    {
        public DbSet<Cheese> Cheeses { get; set; }
        public DbSet<CheeseCategory> Categories { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<CheeseMenu> CheeseMenus { get; set;}
        
        //DbSet part of the Entity framework

        //public CheeseDbContext(DbContextOptions<CheeseDbContext> options) : base(options)
        //{ microsoft/windows
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CheeseMenu>().HasKey(c => new { c.CheeseID, c.MenuID });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=CheeseMVC.db");
        
    }
}

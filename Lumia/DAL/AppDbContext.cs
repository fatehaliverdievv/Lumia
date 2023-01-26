using Lumia.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Reflection.Emit;

namespace Lumia.DAL
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
           
        }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees  { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Position>().HasIndex(p => p.Name).IsUnique();
        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Position>().HasIndex(p => p.Name).IsUnique();
            base.OnModelCreating(builder);
        }
    }
}

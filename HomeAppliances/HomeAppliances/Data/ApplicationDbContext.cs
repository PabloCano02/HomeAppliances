using HomeAppliances.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace HomeAppliances.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<HomeApplianceType> HomeApplianceTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

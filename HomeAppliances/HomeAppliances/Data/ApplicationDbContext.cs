using HomeAppliances.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeAppliances.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<HomeAppliance> HomeAppliances { get; set; }
        public DbSet<HomeApplianceType> HomeApplianceTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HomeAppliance>()
                .HasOne(bc => bc.HomeApplianceType)
                .WithMany(b => b.HomeAppliances)
                .HasForeignKey(bc => bc.HomeApplianceTypeId);
            modelBuilder.Entity<HomeAppliance>()
                .HasOne(bc => bc.Brand)
                .WithMany(b => b.HomeAppliances)
                .HasForeignKey(bc => bc.BrandId);
        }
    }
}

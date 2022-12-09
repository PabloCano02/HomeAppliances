using HomeAppliances.Entities;
using HomeAppliances.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeAppliances.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<HomeAppliance> HomeAppliances { get; set; }
        public DbSet<HomeAppliancePhoto> HomeAppliancePhotos { get; set; }
        public DbSet<HomeApplianceType> HomeApplianceTypes { get; set; }
        public DbSet<IdentityModel> IdentityModels { get; set; }

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
            modelBuilder.Entity<HomeAppliance>()
                .HasOne(bc => bc.IdentityModel)
                .WithMany(b => b.HomeAppliances)
                .HasForeignKey(bc => bc.UserId);

            modelBuilder.Entity<HomeAppliancePhoto>()
                .HasOne(bc => bc.HomeAppliance)
                .WithMany(b => b.HomeAppliancePhotos)
                .HasForeignKey(bc => bc.HomeApplianceId);

            modelBuilder.Entity<IdentityModel>()
                .HasOne(bc => bc.DocumentType)
                .WithMany(b => b.IdentityModels)
                .HasForeignKey(bc => bc.DocumentTypeId);
        }
    }
}

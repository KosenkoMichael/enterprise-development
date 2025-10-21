using CarRentalService.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace CarRentalService.Infrastructure;
public class CarRentalDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<VehicleModel> VehicleModels { get; set; }
    public DbSet<ModelGeneration> ModelGenerations { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Rental> Rentals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ModelGeneration>()
            .ToCollection("model-generations")
            .HasOne(x => x.VehicleModel)
            .WithMany(x => x.ModelGenerations)
            .HasForeignKey(mg => mg.VehicleModelId)
            .HasPrincipalKey(mg => mg.Id);

        modelBuilder.Entity<Vehicle>()
            .ToCollection("vehicles")
            .HasOne(x => x.ModelGeneration)
            .WithMany(x => x.Vehicles)
            .HasForeignKey(v => v.ModelGenerationId)
            .HasPrincipalKey(v => v.Id);

        modelBuilder.Entity<Rental>(o =>
        {
            o.HasOne(x => x.Vehicle)
                .WithMany(x => x.Rentals)
                .HasForeignKey(v => v.VehicleId)
                .HasPrincipalKey(v => v.Id);

            o.HasOne(x => x.Customer)
                .WithMany(x => x.Rentals)
                .HasForeignKey(v => v.CustomerId)
                .HasPrincipalKey(v => v.Id);

            o.ToCollection("rentals");
        });

        Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
        modelBuilder.Entity<VehicleModel>().ToCollection("vehice-models");
        modelBuilder.Entity<Customer>().ToCollection("customers");
    }
}

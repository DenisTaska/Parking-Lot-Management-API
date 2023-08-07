using Microsoft.EntityFrameworkCore;
using Parking_Lot_Management.API.Entities;
using Parking_Lot_Management.API.Models;

namespace Parking_Lot_Management.API.DbContexts;

public class DataContext : DbContext
{
    public DbSet<Log> Logs { get; set; } = null!;
    public DbSet<ParkingSpot> ParkingSpots { get; set; } = null!;

    public DbSet<PricingPlan> PricingPlans { get; set; } = null!;

    public DbSet<Subscriber> Subscribers { get; set; } = null!;

    public DbSet<Subscription> Subscriptions { get; set; } = null!;


    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        // 
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=parking_lot_management_db;User Id=;Password=;");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //
    }
}
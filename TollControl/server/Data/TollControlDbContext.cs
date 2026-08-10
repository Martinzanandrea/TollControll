using Microsoft.EntityFrameworkCore;
using TollControl.Api.Models;

namespace TollControl.Api.Data;

public class TollControlDbContext : DbContext
{
    public TollControlDbContext(DbContextOptions<TollControlDbContext> options) : base(options) { }

    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<VehicleCategory> VehicleCategories => Set<VehicleCategory>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Station> Stations => Set<Station>();
    public DbSet<Lane> Lanes => Set<Lane>();
    public DbSet<Tariff> Tariffs => Set<Tariff>();
    public DbSet<TollTransaction> TollTransactions => Set<TollTransaction>();
    public DbSet<BalanceMovement> BalanceMovements => Set<BalanceMovement>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<Incident> Incidents => Set<Incident>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ---------- ROLE / USER ----------
        modelBuilder.Entity<Role>(e =>
        {
            e.HasIndex(x => x.Name).IsUnique();
        });

        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("user"); // "user" es palabra reservada en Postgres, EF la cita automáticamente
            e.HasIndex(x => x.Username).IsUnique();
            e.HasIndex(x => x.Email).IsUnique();
            e.HasOne(x => x.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ---------- CUSTOMER / ACCOUNT ----------
        modelBuilder.Entity<Customer>(e =>
        {
            e.HasIndex(x => x.UserId).IsUnique();      // User 0..1 Customer
            e.HasIndex(x => x.DocumentId).IsUnique();
            e.HasOne(x => x.User)
                .WithOne(u => u.Customer)
                .HasForeignKey<Customer>(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Account>(e =>
        {
            e.HasIndex(x => x.CustomerId).IsUnique();  // 1:1 con Customer
            e.Property(x => x.Balance).HasColumnType("numeric(12,2)");
            e.HasOne(x => x.Customer)
                .WithOne(c => c.Account)
                .HasForeignKey<Account>(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ---------- VEHICLE_CATEGORY / VEHICLE / TAG ----------
        modelBuilder.Entity<VehicleCategory>(e =>
        {
            e.HasIndex(x => x.Name).IsUnique();
        });

        modelBuilder.Entity<Vehicle>(e =>
        {
            e.HasIndex(x => x.LicensePlate).IsUnique();
            e.HasOne(x => x.VehicleCategory)
                .WithMany(c => c.Vehicles)
                .HasForeignKey(x => x.VehicleCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Tag>(e =>
        {
            e.HasIndex(x => x.SerialCode).IsUnique();

            // Regla de negocio: un vehículo tiene como máximo un TAG en estado ACTIVO
            e.HasIndex(x => x.VehicleId)
                .IsUnique()
                .HasFilter("status = 'ACTIVO'");

            e.HasOne(x => x.Account)
                .WithMany(a => a.Tags)
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Vehicle)
                .WithMany(v => v.Tags)
                .HasForeignKey(x => x.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ---------- STATION / LANE / TARIFF ----------
        modelBuilder.Entity<Lane>(e =>
        {
            e.HasIndex(x => new { x.StationId, x.Number }).IsUnique();
            e.HasOne(x => x.Station)
                .WithMany(s => s.Lanes)
                .HasForeignKey(x => x.StationId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Tariff>(e =>
        {
            e.Property(x => x.Amount).HasColumnType("numeric(12,2)");

            // Evita dos tarifas vigentes (valid_to NULL) para la misma estación+categoría
            e.HasIndex(x => new { x.StationId, x.VehicleCategoryId })
                .IsUnique()
                .HasFilter("valid_to IS NULL");

            e.HasOne(x => x.Station)
                .WithMany(s => s.Tariffs)
                .HasForeignKey(x => x.StationId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.VehicleCategory)
                .WithMany(c => c.Tariffs)
                .HasForeignKey(x => x.VehicleCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ---------- TOLL_TRANSACTION / BALANCE_MOVEMENT ----------
        modelBuilder.Entity<TollTransaction>(e =>
        {
            e.Property(x => x.Amount).HasColumnType("numeric(12,2)");
            e.Property(x => x.BalanceBefore).HasColumnType("numeric(12,2)");
            e.Property(x => x.BalanceAfter).HasColumnType("numeric(12,2)");

            e.HasOne(x => x.Tag)
                .WithMany(t => t.TollTransactions)
                .HasForeignKey(x => x.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Station)
                .WithMany(s => s.TollTransactions)
                .HasForeignKey(x => x.StationId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Lane)
                .WithMany(l => l.TollTransactions)
                .HasForeignKey(x => x.LaneId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Tariff)
                .WithMany(t => t.TollTransactions)
                .HasForeignKey(x => x.TariffId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<BalanceMovement>(e =>
        {
            e.Property(x => x.Amount).HasColumnType("numeric(12,2)");
            e.Property(x => x.BalanceBefore).HasColumnType("numeric(12,2)");
            e.Property(x => x.BalanceAfter).HasColumnType("numeric(12,2)");

            // 1:0..1 con TollTransaction (nullable + único)
            e.HasIndex(x => x.TollTransactionId).IsUnique();

            e.HasOne(x => x.Account)
                .WithMany(a => a.BalanceMovements)
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.TollTransaction)
                .WithOne(t => t.BalanceMovement)
                .HasForeignKey<BalanceMovement>(x => x.TollTransactionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ---------- AUDIT_LOG ----------
        modelBuilder.Entity<AuditLog>(e =>
        {
            e.Property(x => x.OldValue).HasColumnType("jsonb");
            e.Property(x => x.NewValue).HasColumnType("jsonb");

            e.HasOne(x => x.User)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ---------- INCIDENT ----------
        modelBuilder.Entity<Incident>(e =>
        {
            e.HasOne(x => x.Station)
                .WithMany(s => s.Incidents)
                .HasForeignKey(x => x.StationId)
                .OnDelete(DeleteBehavior.SetNull);

            e.HasOne(x => x.Lane)
                .WithMany(l => l.Incidents)
                .HasForeignKey(x => x.LaneId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }
}

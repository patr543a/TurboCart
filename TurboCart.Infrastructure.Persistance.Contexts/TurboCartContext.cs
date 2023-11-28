using Microsoft.EntityFrameworkCore;
using TurboCart.Domain.Entities;

namespace TurboCart.Infrastructure.Persistance.Contexts;

public class TurboCartContext
    : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TurboCartDb;Trusted_Connection=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(c =>
        {
            c.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            c.HasMany(c => c.Bookings)
                .WithOne(b => b.Customer)
                    .HasForeignKey(b => b.CustomerId)
                    .HasPrincipalKey(c => c.CustomerId)
                    .IsRequired();
        });

        modelBuilder.Entity<Booking>(b =>
        {
            b.Property(b => b.Start)
                .IsRequired();
        });
    }
}

using Microsoft.EntityFrameworkCore;
using TurboCart.Domain.Entities;

namespace TurboCart.Infrastructure.Persistance.Contexts;

public class TurboCartContext
    : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb\\mssqllocaldb;Database=TurboCartDb;Trusted_Connection=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Customer>()
                .HasMany(c => c.Bookings)
                    .WithOne(b => b.Customer)
                        .HasForeignKey(b => b.CustomerId)
                        .HasPrincipalKey(c => c.CustomerId);
    }
}

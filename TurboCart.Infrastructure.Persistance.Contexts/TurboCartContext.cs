using Microsoft.EntityFrameworkCore;
using TurboCart.Domain.Entities;

namespace TurboCart.Infrastructure.Persistance.Contexts;

public class TurboCartContext
    : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<User> Users { get; set; }

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

            c.HasData(
                new Customer()
                {
                    CustomerId = 1,
                    Name = "TestCustomer",
                    Bookings = [],
                });
        });

        modelBuilder.Entity<Booking>(b =>
        {
            b.Property(b => b.Start)
                .IsRequired();

            b.HasData(
                new Booking()
                {
                    BookingId = 1,
                    Start = DateTime.Now,
                    CustomerId = 1,
                });
        });

        modelBuilder.Entity<User>(u =>
        {
            u.HasKey(u => u.Username);

            u.Property(u => u.Username)
                .HasMaxLength(50)
                .IsRequired();

            u.Property(u => u.Password)
                .HasMaxLength(20)
                .IsRequired();

            u.HasData(
                new User()
                {
                    Username = "admin",
                    Password = "1234",
                });
        });

        modelBuilder.Entity<DeletedBooking>(d =>
        {
            d.Property(b => b.BookingId)
                .IsRequired();

            d.Property(b => b.CustomerId)
                .IsRequired();

            d.Property(b => b.Start)
                .IsRequired();

            d.Property(b => b.Reason)
                .IsRequired();
            
            d.HasOne(d => d.Customer)
                .WithMany()
                    .IsRequired();
        });
    }
}

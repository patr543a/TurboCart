namespace TurboCart.Domain.Entities;

public class Customer
{
    public int CustomerId { get; set; }
    public string? Name { get; set; }
    public List<Booking>? Bookings { get; set; }
}

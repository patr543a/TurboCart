using System.Text.Json.Serialization;

namespace TurboCart.Domain.Entities;

public class Booking
{
    public int BookingId { get; set; }
    public DateTime Start { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
}

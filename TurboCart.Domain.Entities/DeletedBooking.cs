namespace TurboCart.Domain.Entities;

public class DeletedBooking()
{
    public int DeletedBookingId { get; set; }
    public int BookingId { get; set; }
    public DateTime Start { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public string? Reason { get; set; }

    public DeletedBooking(Booking booking, string? reason = null)
        : this()
    {
        BookingId = booking.BookingId;
        Start = booking.Start;
        CustomerId = booking.CustomerId;
        Customer = booking.Customer;
        Reason = reason;
    }
}

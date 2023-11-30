using Microsoft.AspNetCore.Mvc;
using TurboCart.Application.Interfaces;
using TurboCart.Domain.Entities;

namespace TurboCart.Presentation.Apis.TurboCart.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController(IBookingUseCase _bookingUseCase)
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
    {
        var bookings = await _bookingUseCase.GetAllBookings();

        return Ok(bookings ?? []);
    }

    [HttpGet("Today")]
    public async Task<ActionResult<IEnumerable<Booking>>> GetTodaysBookings()
    {
        var bookings = await _bookingUseCase.GetTodaysBookings();

        return Ok(bookings ?? []);
    }

    [HttpGet("Date/{date}")]
    public async Task<ActionResult<IEnumerable<Booking>>> GetBookingsForDate(DateOnly date)
    {
        var bookings = await _bookingUseCase.GetBookingsForDate(date);

        return Ok(bookings ?? []);
    }

    [HttpGet("Week")]
    public async Task<ActionResult<IEnumerable<Booking>>> GetThisWeeksBookings()
    {
        var bookings = await _bookingUseCase.GetThisWeeksBookings();

        return Ok(bookings ?? []);
    }

    [HttpGet("{bookingId}")]
    public async Task<ActionResult<Booking>> GetBooking(int bookingId)
    {
        var booking = await _bookingUseCase.GetBooking(bookingId);

        if (booking is null)
            return NotFound();

        return Ok(booking);
    }

    [HttpPost]
    public async Task<ActionResult<Booking>> PostBooking([FromBody] Booking booking)
    {
        try
        {
            await _bookingUseCase.AddBooking(booking);
        }
        catch
        {
            return BadRequest();
        }

        return Ok(booking);
    }

    [HttpPut]
    public async Task<ActionResult<Booking>> PutBooking([FromBody] Booking booking)
    {
        try
        {
            await _bookingUseCase.UpdateBooking(booking);
        }
        catch
        {
            return BadRequest();
        }

        return Ok(booking);
    }

    [HttpPost("{bookingId}")]
    public async Task<ActionResult> DeleteBooking(int bookingId, DeletedBooking deletedBooking)
    {
        DeletedBooking? result = null;

        try
        {
            result = await _bookingUseCase.DeleteBooking(bookingId, deletedBooking.Reason!);
        }
        catch
        {
            return BadRequest();
        }

        return Ok(result);
    }
}
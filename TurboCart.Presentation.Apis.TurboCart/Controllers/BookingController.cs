﻿using Microsoft.AspNetCore.Mvc;
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
        var bookings = _bookingUseCase.GetAllBookings();

        return Ok(await bookings ?? []);
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

    [HttpDelete("{bookingId}")]
    public async Task<ActionResult> DeleteBooking(int bookingId)
    {
        try
        {
            await _bookingUseCase.DeleteBooking(bookingId);
        }
        catch
        {
            return BadRequest();
        }

        return Ok();
    }
}
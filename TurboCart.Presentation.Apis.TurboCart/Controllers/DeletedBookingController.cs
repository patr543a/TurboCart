using Microsoft.AspNetCore.Mvc;
using TurboCart.Application.Interfaces;
using TurboCart.Domain.Entities;

namespace TurboCart.Presentation.Apis.TurboCart.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeletedBookingController(IDeletedBookingUseCase _deletedBookingUseCase)
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DeletedBooking>>> GetDeletedBookings()
    {
        var deletedBookings = await _deletedBookingUseCase.GetDeletedBookings();

        return Ok(deletedBookings ?? []);
    }

    [HttpGet("by/{customerId}")]
    public async Task<ActionResult<IEnumerable<DeletedBooking>>> GetDeletedBookingsByCustomer(int customerId)
    {
        var deletedBookings = await _deletedBookingUseCase.GetDeletedBookingsByCustomer(customerId);

        return Ok(deletedBookings ?? []);
    }

    [HttpGet("{deletedBookingId}")]
    public async Task<ActionResult<DeletedBooking>> GetDeletedBooking(int deletedBookingId)
    {
        var deletedBooking = await _deletedBookingUseCase.GetDeletedBooking(deletedBookingId);

        return Ok(deletedBooking);
    }

    [HttpPost]
    public async Task<ActionResult<DeletedBooking>> PostDeletedBooking([FromBody] DeletedBooking deletedBooking)
    {
        try
        {
            await _deletedBookingUseCase.AddDeletedBooking(deletedBooking);
        }
        catch
        {
            return BadRequest();
        }

        return Ok(deletedBooking);
    }
}

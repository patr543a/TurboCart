using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TurboCart.Application.Interfaces;
using TurboCart.Domain.Entities;

namespace TurboCart.Presentation.Websites.TurboCartDK.Pages;

public class IndexModel(IBookingUseCase _bookingUseCase)
    : PageModel
{
    public IEnumerable<Booking>? Bookings { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Bookings = await _bookingUseCase.GetAllBookings();

        return Page();
    }
}

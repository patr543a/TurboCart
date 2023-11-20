using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TurboCart.Application.Interfaces;
using TurboCart.Application.UseCases;

namespace TurboCart.Tests.UoWRazorTestApp.Pages;

public class IndexModel(ILogger<IndexModel> _logger, IBookingUseCase _bookingUseCase) : PageModel
{
    public IBookingUseCase BookingUseCase => _bookingUseCase;

    public void OnGet()
    {

    }
}

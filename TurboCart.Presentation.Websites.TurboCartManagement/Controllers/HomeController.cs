using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TurboCart.Application.Interfaces;
using TurboCart.Domain.Entities;
using TurboCart.Presentation.Websites.TurboCartManagement.Models;

namespace TurboCart.Presentation.Websites.TurboCartManagement.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBookingUseCase _bookingUseCase;

    public HomeController(ILogger<HomeController> logger, IBookingUseCase bookingUseCase)
    {
        _logger = logger;
        _bookingUseCase = bookingUseCase;
    }

    public async Task<IActionResult> Index()
    {
        var model = await _bookingUseCase.GetAllBookings();
        return View(model);
    }

    public async Task<IActionResult> Today() {
        var model = await _bookingUseCase.GetTodaysBookings();
        return View("Index", model);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id) {
        var model = await _bookingUseCase.GetBooking(id);
        return View(model);
    }



    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

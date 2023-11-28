using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TurboCart.Domain.Entities;
using TurboCart.Presentation.Websites.TurboCartManagement.Models;

namespace TurboCart.Presentation.Websites.TurboCartManagement.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var model = new List<Booking> {
            new Booking { BookingId = 1, Start = DateTime.Now, CustomerId = 1, Customer = new Customer() { CustomerId = 1, Name = "Sean" } },
            new Booking { BookingId = 2, Start = DateTime.Now, CustomerId = 2, Customer = new Customer() { CustomerId = 2, Name = "Dave" } }
        };

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

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
    private readonly ICustomerUseCase _customerUseCase;

    public HomeController(ILogger<HomeController> logger, IBookingUseCase bookingUseCase, ICustomerUseCase customerUseCase)
    {
        _logger = logger;
        _bookingUseCase = bookingUseCase;
        _customerUseCase = customerUseCase;
    }

    public async Task<IActionResult> Index()
    {
        var model = await _bookingUseCase.GetTodaysBookings();
        return View(model);
    }
    
    [HttpGet("{date}")]
    public async Task<IActionResult> ListDay(DateOnly date) {
        var model = await _bookingUseCase.GetBookingsForDate(date);
        return View("Index", model);
    }

    [HttpGet("week")]
    public async Task<IActionResult> ListThisWeek() {
        var model = await _bookingUseCase.GetThisWeeksBookings();
        return View("Index", model);
    }

    [HttpGet("all")]
    public async Task<IActionResult> ListAll()
    {
        var model = await _bookingUseCase.GetAllBookings();
        return View("Index", model);
    }


    [HttpGet("details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var model = await _bookingUseCase.GetBooking(id);
        if (model == null) {
            return NotFound($"Booking med id {{id}} kunne ikke findes");
        }
        return View(model);
    }


    [HttpGet("new")]
    public async Task<IActionResult> CreateNew()
    {
        var model = new BookingViewModel() { AvailableCustomers = await _customerUseCase.GetAllCustomers() };
        return View(model);
    }

    [HttpPost("new")]
    public async Task<IActionResult> CreateNew(BookingViewModel bookingViewModel) {
        if (!ModelState.IsValid) {
            return BadRequest("Alle felter er påkrævet");
        }

        Booking b = new() {
            BookingId = bookingViewModel.BookingId,
            Start = new DateTime(bookingViewModel.Date, bookingViewModel.Time),
            CustomerId = bookingViewModel.CustomerId
        };
        var result = await _bookingUseCase.AddBooking(b);
        if (result == null) {
            return BadRequest("Fik ugyldigt svar tilbage fra serveren");
        }

        return Redirect("/");
    }


    [HttpGet("edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var b = await _bookingUseCase.GetBooking(id);
        if (b == null) {
            return NotFound($"Booking med id {id} kunne ikke findes");
        }

        var model = new BookingViewModel()
        {
            BookingId = b.BookingId,
            Date = DateOnly.FromDateTime(b.Start),
            Time = TimeOnly.FromDateTime(b.Start),
            CustomerId = b.CustomerId,
            AvailableCustomers = await _customerUseCase.GetAllCustomers()
        };

        return View(model);
    }

    [HttpPost("edit/{id}")]
    public async Task<IActionResult> Save(BookingViewModel bookingViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Alle felter er påkrævet");
        }
        Booking b = new()
        {
            BookingId = bookingViewModel.BookingId,
            Start = new DateTime(bookingViewModel.Date, bookingViewModel.Time),
            CustomerId = bookingViewModel.CustomerId
        };

        var result = await _bookingUseCase.UpdateBooking(b);
        if (result == null) {
            return BadRequest("Fik ugyldigt svar tilbage fra serveren");
        }

        return Redirect("/");
    }


    [HttpGet("delete/{id}")]
    public IActionResult Delete(int id) {
        var model = new DeleteReasonViewModel() {
            BookingId = id
        };

        return View(model);
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete(DeleteReasonViewModel deleteReasonViewModel) {
        string reason = deleteReasonViewModel.Reason;
        if (deleteReasonViewModel.Reason == "") {
            reason = deleteReasonViewModel.DetailedReason;
        }

        var result = await _bookingUseCase.DeleteBooking(deleteReasonViewModel.BookingId, reason);
        if (result == null) {
            return BadRequest("Fik ugyldigt svar tilbage fra serveren");
        }

        return Redirect("/");
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

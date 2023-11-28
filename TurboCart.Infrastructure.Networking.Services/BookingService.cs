﻿using TurboCart.Application.Interfaces;
using TurboCart.Domain.Entities;

namespace TurboCart.Infrastructure.Networking.Services;

public class BookingService
    : ServiceBase, IBookingUseCase
{
    public BookingService()
        : base(@"http://localhost:5082/")
    {
    }

    public async Task<Booking?> AddBooking(Booking booking)
        => await PostAsJsonAsync("api/Booking", booking);

    public async Task<int?> DeleteBooking(int bookingId)
        => await DeleteFromJsonAsync<int>($"api/Booking/{bookingId}");

    public async Task<IEnumerable<Booking>?> GetAllBookings()
        => await GetFromJsonAsync<IEnumerable<Booking>>("api/Booking");

    public async Task<Booking?> GetBooking(int bookingId)
        => await GetFromJsonAsync<Booking>($"api/Booking/{bookingId}");

    public async Task<IEnumerable<Booking>?> GetTodaysBookings()
        => throw new NotImplementedException();

    public async Task<bool?> IsValidBooking(Booking booking)
        => throw new NotImplementedException();

    public async Task<Booking?> UpdateBooking(Booking booking)
        => await PutAsJsonAsync("api/Booking", booking);
}

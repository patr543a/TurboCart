using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TurboCart.Domain.Entities;

namespace TurboCart.Presentation.Websites.TurboCartManagement.Models
{
    public class BookingViewModel {
        public int BookingId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public int CustomerId { get; set; }
        public IEnumerable<Customer>? AvailableCustomers { get; set; }
    }
}

using HotelManagementSystem.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Domain
{
    public class Invoice
    {
        public int InvoiceID { get; set; }

        [Required]
        public int BookingID { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public DateTime GeneratedDate { get; set; } = DateTime.Now;

        public bool PaidStatus { get; set; } = false;

        // Navigation
        public Booking? Booking { get; set; }
    }
}
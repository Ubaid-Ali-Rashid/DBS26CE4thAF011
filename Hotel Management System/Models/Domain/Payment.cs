using HotelManagementSystem.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Domain
{
    public class Payment
    {
        public int PaymentID { get; set; }

        [Required]
        public int BookingID { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Payment method is required")]
        public string Method { get; set; }

        public string Status { get; set; } = "Paid";

        // Navigation
        public Booking? Booking { get; set; }
    }
}
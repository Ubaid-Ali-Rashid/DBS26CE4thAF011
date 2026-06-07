using HotelManagementSystem.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Domain
{
    public class Cancellation
    {
        public int CancellationID { get; set; }

        [Required]
        public int BookingID { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        [StringLength(255)]
        public string Reason { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Refund amount cannot be negative")]
        public decimal RefundAmount { get; set; } = 0;

        public DateTime CancelDate { get; set; } = DateTime.Now;

        // Navigation
        public Booking? Booking { get; set; }
    }
}
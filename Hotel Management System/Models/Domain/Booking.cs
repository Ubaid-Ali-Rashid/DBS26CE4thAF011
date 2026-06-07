using HotelManagementSystem.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Domain
{
    public class Booking
    {
        public int BookingID { get; set; }

        [Required]
        public int GuestID { get; set; }

        [Required]
        public int RoomID { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Check-in date is required")]
        public DateTime CheckIn { get; set; }

        [Required(ErrorMessage = "Check-out date is required")]
        public DateTime CheckOut { get; set; }

        public string Status { get; set; } = "Confirmed";

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total amount cannot be negative")]
        public decimal TotalAmount { get; set; }

        // Navigation
        public Guest? Guest { get; set; }
        public Room? Room { get; set; }
    }
}
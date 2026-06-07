using HotelManagementSystem.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Domain
{
    public class RoomService
    {
        public int ServiceID { get; set; }

        [Required]
        public int BookingID { get; set; }

        [Required]
        public int ServiceTypeID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; } = 1;

        public DateTime RequestTime { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pending";

        // Navigation
        public Booking? Booking { get; set; }
        public ServiceType? ServiceType { get; set; }
    }
}
using HotelManagementSystem.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Domain
{
    public class Room
    {
        public int RoomID { get; set; }

        [Required(ErrorMessage = "Room number is required")]
        [StringLength(10)]
        public string RoomNumber { get; set; }

        [Required]
        public int TypeID { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Floor cannot be negative")]
        public int Floor { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal PricePerNight { get; set; }

        [Required]
        public string Status { get; set; } = "Available";

        // Navigation
        public RoomType? RoomType { get; set; }
    }
}
using HotelManagementSystem.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Domain
{
    public class Housekeeping
    {
        public int TaskID { get; set; }

        [Required]
        public int RoomID { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Scheduled date is required")]
        public DateTime ScheduledDate { get; set; }

        public string Status { get; set; } = "Pending";

        [StringLength(255)]
        public string? Notes { get; set; }

        // Navigation
        public Room? Room { get; set; }
        public Employee? Employee { get; set; }
    }
}
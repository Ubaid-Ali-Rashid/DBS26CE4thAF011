using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Domain
{
    public class ErrorLog
    {
        public int LogID { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }

        public string? StackTrace { get; set; }

        public DateTime LogTime { get; set; } = DateTime.Now;

        public int? UserID { get; set; }
    }
}
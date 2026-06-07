using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Domain
{
    public class Guest
    {
        public int GuestID { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "CNIC is required")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "CNIC must be exactly 13 digits")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "CNIC must contain only digits")]
        public string CNIC { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.Now;
    }
}
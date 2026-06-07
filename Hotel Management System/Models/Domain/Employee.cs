using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Domain
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "CNIC is required")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "CNIC must be exactly 13 digits")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "CNIC must contain only digits")]
        public string CNIC { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(15)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Salary must be greater than 0")]
        public decimal Salary { get; set; }

        [Required]
        public DateTime HireDate { get; set; } = DateTime.Today;
    }
}
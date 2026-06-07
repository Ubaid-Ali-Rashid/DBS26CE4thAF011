using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Domain
{
    public class ServiceType
    {
        public int ServiceTypeID { get; set; }

        [Required(ErrorMessage = "Service name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price cannot be negative")]
        public decimal Price { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Domain
{
    public class Amenity
    {
        public int AmenityID { get; set; }

        [Required(ErrorMessage = "Amenity name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }
    }
}
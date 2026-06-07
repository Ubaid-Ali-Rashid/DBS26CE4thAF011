using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Domain
{
    public class RoomType
    {
        public int TypeID { get; set; }

        [Required(ErrorMessage = "Type name is required")]
        [StringLength(50)]
        public string TypeName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
        public int Capacity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Base price must be greater than 0")]
        public decimal BasePrice { get; set; }
    }
}
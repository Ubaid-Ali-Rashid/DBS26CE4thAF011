using HotelManagementSystem.Models.Domain;

namespace HotelManagementSystem.Models.Domain
{
    public class RoomAmenity
    {
        public int RoomID { get; set; }
        public int AmenityID { get; set; }

        // Navigation
        public Room? Room { get; set; }
        public Amenity? Amenity { get; set; }
    }
}
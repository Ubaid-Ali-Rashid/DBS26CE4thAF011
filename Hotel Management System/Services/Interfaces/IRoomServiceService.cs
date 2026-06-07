using HotelManagementSystem.Models.Domain;

namespace HotelManagementSystem.Services.Interfaces
{
    public interface IRoomServiceService
    {
        Task<IEnumerable<RoomService>> GetAllRoomServicesAsync();
        Task<RoomService?> GetRoomServiceByIdAsync(int serviceId);
        Task<int> AddRoomServiceAsync(RoomService roomService);
        Task<bool> UpdateRoomServiceAsync(RoomService roomService);
        Task<IEnumerable<RoomService>> GetRoomServicesByBookingIdAsync(int bookingId);
    }
}
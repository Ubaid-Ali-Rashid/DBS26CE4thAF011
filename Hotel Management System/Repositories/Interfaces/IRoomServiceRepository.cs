using HotelManagementSystem.Models.Domain;

namespace HotelManagementSystem.Repositories.Interfaces
{
    public interface IRoomServiceRepository
    {
        Task<IEnumerable<RoomService>> GetAllAsync();
        Task<RoomService?> GetByIdAsync(int serviceId);
        Task<int> AddAsync(RoomService roomService);
        Task<bool> UpdateAsync(RoomService roomService);
        Task<IEnumerable<RoomService>> GetByBookingIdAsync(int bookingId);
    }
}
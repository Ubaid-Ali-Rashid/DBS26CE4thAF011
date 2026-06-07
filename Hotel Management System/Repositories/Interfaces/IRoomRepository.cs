using HotelManagementSystem.Models.Domain;

namespace HotelManagementSystem.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room?> GetByIdAsync(int roomId);
        Task<int> AddAsync(Room room);
        Task<bool> UpdateAsync(Room room);
        Task<bool> DeleteAsync(int roomId);
        Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut);
    }
}
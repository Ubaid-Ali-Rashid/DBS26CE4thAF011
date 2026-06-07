using HotelManagementSystem.Models.Domain;

namespace HotelManagementSystem.Repositories.Interfaces
{
    public interface IHousekeepingRepository
    {
        Task<IEnumerable<Housekeeping>> GetAllAsync();
        Task<Housekeeping?> GetByIdAsync(int taskId);
        Task<int> AddAsync(Housekeeping housekeeping);
        Task<bool> UpdateAsync(Housekeeping housekeeping);
        Task<IEnumerable<Housekeeping>> GetByRoomIdAsync(int roomId);
    }
}
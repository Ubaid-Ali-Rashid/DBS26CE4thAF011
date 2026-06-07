using HotelManagementSystem.Models.Domain;

namespace HotelManagementSystem.Services.Interfaces
{
    public interface IHousekeepingService
    {
        Task<IEnumerable<Housekeeping>> GetAllTasksAsync();
        Task<Housekeeping?> GetTaskByIdAsync(int taskId);
        Task<int> AddTaskAsync(Housekeeping housekeeping);
        Task<bool> UpdateTaskAsync(Housekeeping housekeeping);
        Task<IEnumerable<Housekeeping>> GetTasksByRoomIdAsync(int roomId);
    }
}
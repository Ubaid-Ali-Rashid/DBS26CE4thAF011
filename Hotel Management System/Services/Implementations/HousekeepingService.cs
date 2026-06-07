using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Repositories.Interfaces;
using HotelManagementSystem.Services.Interfaces;

namespace HotelManagementSystem.Services.Implementations
{
    public class HousekeepingService : IHousekeepingService
    {
        private readonly IHousekeepingRepository _repo;
        public HousekeepingService(IHousekeepingRepository repo) { _repo = repo; }

        public Task<IEnumerable<Housekeeping>> GetAllTasksAsync() => _repo.GetAllAsync();
        public Task<Housekeeping?> GetTaskByIdAsync(int taskId) => _repo.GetByIdAsync(taskId);
        public Task<int> AddTaskAsync(Housekeeping housekeeping) => _repo.AddAsync(housekeeping);
        public Task<bool> UpdateTaskAsync(Housekeeping housekeeping) => _repo.UpdateAsync(housekeeping);
        public Task<IEnumerable<Housekeeping>> GetTasksByRoomIdAsync(int roomId) =>
            _repo.GetByRoomIdAsync(roomId);
    }
}
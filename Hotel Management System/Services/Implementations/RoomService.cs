using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Repositories.Interfaces;
using HotelManagementSystem.Services.Interfaces;

namespace HotelManagementSystem.Services.Implementations
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repo;
        public RoomService(IRoomRepository repo) { _repo = repo; }

        public Task<IEnumerable<Room>> GetAllRoomsAsync() => _repo.GetAllAsync();
        public Task<Room?> GetRoomByIdAsync(int roomId) => _repo.GetByIdAsync(roomId);
        public Task<int> AddRoomAsync(Room room) => _repo.AddAsync(room);
        public Task<bool> UpdateRoomAsync(Room room) => _repo.UpdateAsync(room);
        public Task<bool> DeleteRoomAsync(int roomId) => _repo.DeleteAsync(roomId);
        public Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut) =>
            _repo.GetAvailableRoomsAsync(checkIn, checkOut);
    }
}
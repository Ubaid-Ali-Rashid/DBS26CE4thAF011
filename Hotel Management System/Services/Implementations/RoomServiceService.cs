using HotelManagementSystem.Repositories.Interfaces;
using HotelManagementSystem.Services.Interfaces;
using DomainRoomService = HotelManagementSystem.Models.Domain.RoomService;

namespace HotelManagementSystem.Services.Implementations
{
    public class RoomServiceService : IRoomServiceService
    {
        private readonly IRoomServiceRepository _repo;
        public RoomServiceService(IRoomServiceRepository repo) { _repo = repo; }

        public Task<IEnumerable<DomainRoomService>> GetAllRoomServicesAsync() => _repo.GetAllAsync();
        public Task<DomainRoomService?> GetRoomServiceByIdAsync(int serviceId) => _repo.GetByIdAsync(serviceId);
        public Task<int> AddRoomServiceAsync(DomainRoomService roomService) => _repo.AddAsync(roomService);
        public Task<bool> UpdateRoomServiceAsync(DomainRoomService roomService) => _repo.UpdateAsync(roomService);
        public Task<IEnumerable<DomainRoomService>> GetRoomServicesByBookingIdAsync(int bookingId) =>
            _repo.GetByBookingIdAsync(bookingId);
    }
}
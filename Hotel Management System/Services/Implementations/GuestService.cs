using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Repositories.Interfaces;
using HotelManagementSystem.Services.Interfaces;

namespace HotelManagementSystem.Services.Implementations
{
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _repo;
        public GuestService(IGuestRepository repo) { _repo = repo; }

        public Task<IEnumerable<Guest>> GetAllGuestsAsync() => _repo.GetAllAsync();
        public Task<Guest?> GetGuestByIdAsync(int guestId) => _repo.GetByIdAsync(guestId);
        public Task<int> AddGuestAsync(Guest guest) => _repo.AddAsync(guest);
        public Task<bool> UpdateGuestAsync(Guest guest) => _repo.UpdateAsync(guest);
        public Task<bool> DeleteGuestAsync(int guestId) => _repo.DeleteAsync(guestId);
        public Task<Guest?> GetGuestByEmailAsync(string email) => _repo.GetByEmailAsync(email);
    }
}
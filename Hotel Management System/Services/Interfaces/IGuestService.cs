using HotelManagementSystem.Models.Domain;

namespace HotelManagementSystem.Services.Interfaces
{
    public interface IGuestService
    {
        Task<IEnumerable<Guest>> GetAllGuestsAsync();
        Task<Guest?> GetGuestByIdAsync(int guestId);
        Task<int> AddGuestAsync(Guest guest);
        Task<bool> UpdateGuestAsync(Guest guest);
        Task<bool> DeleteGuestAsync(int guestId);
        Task<Guest?> GetGuestByEmailAsync(string email);
    }
}
using HotelManagementSystem.Models.Domain;

namespace HotelManagementSystem.Repositories.Interfaces
{
    public interface IGuestRepository
    {
        Task<IEnumerable<Guest>> GetAllAsync();
        Task<Guest?> GetByIdAsync(int guestId);
        Task<int> AddAsync(Guest guest);
        Task<bool> UpdateAsync(Guest guest);
        Task<bool> DeleteAsync(int guestId);
        Task<Guest?> GetByEmailAsync(string email);
    }
}
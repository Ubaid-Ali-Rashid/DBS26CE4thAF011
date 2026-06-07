using HotelManagementSystem.Models.Domain;

namespace HotelManagementSystem.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int bookingId);
        Task<int> AddAsync(Booking booking);
        Task<bool> UpdateAsync(Booking booking);
        Task<bool> DeleteAsync(int bookingId);
        Task<IEnumerable<Booking>> GetByGuestIdAsync(int guestId);
    }
}
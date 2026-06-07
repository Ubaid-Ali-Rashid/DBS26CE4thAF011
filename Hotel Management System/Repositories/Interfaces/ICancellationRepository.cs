using HotelManagementSystem.Models.Domain;

namespace HotelManagementSystem.Repositories.Interfaces
{
    public interface ICancellationRepository
    {
        Task<IEnumerable<Cancellation>> GetAllAsync();
        Task<Cancellation?> GetByIdAsync(int cancellationId);
        Task<int> AddAsync(Cancellation cancellation);
        Task<Cancellation?> GetByBookingIdAsync(int bookingId);
    }
}
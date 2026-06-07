using HotelManagementSystem.Models.Domain;

namespace HotelManagementSystem.Services.Interfaces
{
    public interface ICancellationService
    {
        Task<IEnumerable<Cancellation>> GetAllCancellationsAsync();
        Task<Cancellation?> GetCancellationByIdAsync(int cancellationId);
        Task<int> AddCancellationAsync(Cancellation cancellation);
        Task<Cancellation?> GetCancellationByBookingIdAsync(int bookingId);
    }
}
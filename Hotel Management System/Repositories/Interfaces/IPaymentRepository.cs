using HotelManagementSystem.Models.Domain;

namespace HotelManagementSystem.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(int paymentId);
        Task<int> AddAsync(Payment payment);
        Task<IEnumerable<Payment>> GetByBookingIdAsync(int bookingId);
    }
}
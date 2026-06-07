using HotelManagementSystem.Models.Domain;

namespace HotelManagementSystem.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment?> GetPaymentByIdAsync(int paymentId);
        Task<int> AddPaymentAsync(Payment payment);
        Task<IEnumerable<Payment>> GetPaymentsByBookingIdAsync(int bookingId);
    }
}
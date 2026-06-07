using HotelManagementSystem.Models.Domain;

namespace HotelManagementSystem.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task<Invoice?> GetByIdAsync(int invoiceId);
        Task<int> AddAsync(Invoice invoice);
        Task<Invoice?> GetByBookingIdAsync(int bookingId);
        Task<bool> UpdateAsync(Invoice invoice);
    }
}
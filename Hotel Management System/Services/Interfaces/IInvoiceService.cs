using HotelManagementSystem.Models.Domain;

namespace HotelManagementSystem.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetAllInvoicesAsync();
        Task<Invoice?> GetInvoiceByIdAsync(int invoiceId);
        Task<int> AddInvoiceAsync(Invoice invoice);
        Task<Invoice?> GetInvoiceByBookingIdAsync(int bookingId);
    }
}
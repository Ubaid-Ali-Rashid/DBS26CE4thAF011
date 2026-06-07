using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Repositories.Interfaces;
using HotelManagementSystem.Services.Interfaces;

namespace HotelManagementSystem.Services.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _repo;
        private readonly IBookingRepository _bookingRepo;

        public InvoiceService(IInvoiceRepository repo, IBookingRepository bookingRepo)
        {
            _repo = repo;
            _bookingRepo = bookingRepo;
        }

        public Task<IEnumerable<Invoice>> GetAllInvoicesAsync() => _repo.GetAllAsync();
        public Task<Invoice?> GetInvoiceByIdAsync(int invoiceId) => _repo.GetByIdAsync(invoiceId);
        public Task<Invoice?> GetInvoiceByBookingIdAsync(int bookingId) => _repo.GetByBookingIdAsync(bookingId);

        public async Task<int> AddInvoiceAsync(Invoice invoice)
        {
            var booking = await _bookingRepo.GetByIdAsync(invoice.BookingID);
            if (booking != null)
            {
                invoice.TotalAmount = booking.TotalAmount;
            }
            return await _repo.AddAsync(invoice);
        }
    }
}

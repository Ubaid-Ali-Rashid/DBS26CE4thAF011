using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Repositories.Interfaces;
using HotelManagementSystem.Services.Interfaces;

namespace HotelManagementSystem.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repo;
        private readonly IBookingRepository _bookingRepo;
        private readonly IInvoiceRepository _invoiceRepo;

        public PaymentService(IPaymentRepository repo, IBookingRepository bookingRepo, IInvoiceRepository invoiceRepo)
        {
            _repo = repo;
            _bookingRepo = bookingRepo;
            _invoiceRepo = invoiceRepo;
        }

        public Task<IEnumerable<Payment>> GetAllPaymentsAsync() => _repo.GetAllAsync();
        public Task<Payment?> GetPaymentByIdAsync(int paymentId) => _repo.GetByIdAsync(paymentId);
        public Task<IEnumerable<Payment>> GetPaymentsByBookingIdAsync(int bookingId) =>
            _repo.GetByBookingIdAsync(bookingId);

        public async Task<int> AddPaymentAsync(Payment payment)
        {
            var booking = await _bookingRepo.GetByIdAsync(payment.BookingID);
            if (booking != null)
            {
                payment.Amount = booking.TotalAmount;
            }

            var result = await _repo.AddAsync(payment);

            // Mark invoice as paid
            var invoice = await _invoiceRepo.GetByBookingIdAsync(payment.BookingID);
            if (invoice != null)
            {
                invoice.PaidStatus = true;
                await _invoiceRepo.UpdateAsync(invoice);
            }

            return result;
        }
    }
}

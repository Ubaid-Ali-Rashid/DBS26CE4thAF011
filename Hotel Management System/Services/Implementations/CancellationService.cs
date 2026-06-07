using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Repositories.Interfaces;
using HotelManagementSystem.Services.Interfaces;

namespace HotelManagementSystem.Services.Implementations
{
    public class CancellationService : ICancellationService
    {
        private readonly ICancellationRepository _repo;
        private readonly IBookingRepository _bookingRepo;

        public CancellationService(ICancellationRepository repo, IBookingRepository bookingRepo)
        {
            _repo = repo;
            _bookingRepo = bookingRepo;
        }

        public Task<IEnumerable<Cancellation>> GetAllCancellationsAsync() => _repo.GetAllAsync();
        public Task<Cancellation?> GetCancellationByIdAsync(int cancellationId) => _repo.GetByIdAsync(cancellationId);
        public Task<Cancellation?> GetCancellationByBookingIdAsync(int bookingId) => _repo.GetByBookingIdAsync(bookingId);

        public async Task<int> AddCancellationAsync(Cancellation cancellation)
        {
            var booking = await _bookingRepo.GetByIdAsync(cancellation.BookingID);
            if (booking != null)
            {
                cancellation.RefundAmount = booking.TotalAmount;
                booking.Status = "Cancelled";
                await _bookingRepo.UpdateAsync(booking);
            }
            return await _repo.AddAsync(cancellation);
        }
    }
}

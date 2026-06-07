using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Repositories.Interfaces;
using HotelManagementSystem.Services.Interfaces;

namespace HotelManagementSystem.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repo;
        private readonly IRoomRepository _roomRepo;

        public BookingService(IBookingRepository repo, IRoomRepository roomRepo)
        {
            _repo = repo;
            _roomRepo = roomRepo;
        }

        public Task<IEnumerable<Booking>> GetAllBookingsAsync() => _repo.GetAllAsync();
        public Task<Booking?> GetBookingByIdAsync(int bookingId) => _repo.GetByIdAsync(bookingId);
        public Task<bool> UpdateBookingAsync(Booking booking) => _repo.UpdateAsync(booking);
        public Task<bool> DeleteBookingAsync(int bookingId) => _repo.DeleteAsync(bookingId);
        public Task<IEnumerable<Booking>> GetBookingsByGuestIdAsync(int guestId) =>
            _repo.GetByGuestIdAsync(guestId);

        public async Task<int> AddBookingAsync(Booking booking)
        {
            booking.CheckIn = booking.CheckIn.Date;
            booking.CheckOut = booking.CheckOut.Date;

            var room = await _roomRepo.GetByIdAsync(booking.RoomID);
            if (room != null)
            {
                int nights = (booking.CheckOut - booking.CheckIn).Days;
                if (nights < 1) nights = 1;
                booking.TotalAmount = room.PricePerNight * nights;
            }
            return await _repo.AddAsync(booking);
        }
    }
}

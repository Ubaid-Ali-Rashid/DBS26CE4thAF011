using Dapper;
using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Repositories.Interfaces;

namespace HotelManagementSystem.Repositories.Implementations
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DbConnection _db;
        public BookingRepository(DbConnection db) { _db = db; }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            using var conn = _db.GetConnection();
            return await conn.QueryAsync<Booking>("SELECT * FROM Booking");
        }

        public async Task<Booking?> GetByIdAsync(int bookingId)
        {
            using var conn = _db.GetConnection();
            return await conn.QueryFirstOrDefaultAsync<Booking>(
                "SELECT * FROM Booking WHERE BookingID = @BookingID", new { BookingID = bookingId });
        }

        public async Task<int> AddAsync(Booking booking)
        {
            using var conn = _db.GetConnection();
            var sql = @"INSERT INTO Booking (GuestID, RoomID, BookingDate, CheckIn, CheckOut, Status, TotalAmount)
                        VALUES (@GuestID, @RoomID, @BookingDate, @CheckIn, @CheckOut, @Status, @TotalAmount);
                        SELECT LAST_INSERT_ID();";
            return await conn.ExecuteScalarAsync<int>(sql, booking);
        }

        public async Task<bool> UpdateAsync(Booking booking)
        {
            using var conn = _db.GetConnection();
            var sql = @"UPDATE Booking SET GuestID=@GuestID, RoomID=@RoomID, CheckIn=@CheckIn,
                        CheckOut=@CheckOut, Status=@Status, TotalAmount=@TotalAmount
                        WHERE BookingID=@BookingID";
            var rows = await conn.ExecuteAsync(sql, booking);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int bookingId)
        {
            using var conn = _db.GetConnection();
            var rows = await conn.ExecuteAsync("DELETE FROM Booking WHERE BookingID = @BookingID", new { BookingID = bookingId });
            return rows > 0;
        }

        public async Task<IEnumerable<Booking>> GetByGuestIdAsync(int guestId)
        {
            using var conn = _db.GetConnection();
            return await conn.QueryAsync<Booking>(
                "SELECT * FROM Booking WHERE GuestID = @GuestID", new { GuestID = guestId });
        }
    }
}
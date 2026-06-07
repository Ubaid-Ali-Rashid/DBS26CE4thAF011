using Dapper;
using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Repositories.Interfaces;

namespace HotelManagementSystem.Repositories.Implementations
{
    public class CancellationRepository : ICancellationRepository
    {
        private readonly DbConnection _db;
        public CancellationRepository(DbConnection db) { _db = db; }

        public async Task<IEnumerable<Cancellation>> GetAllAsync()
        {
            using var conn = _db.GetConnection();
            return await conn.QueryAsync<Cancellation>("SELECT * FROM Cancellation");
        }

        public async Task<Cancellation?> GetByIdAsync(int cancellationId)
        {
            using var conn = _db.GetConnection();
            return await conn.QueryFirstOrDefaultAsync<Cancellation>(
                "SELECT * FROM Cancellation WHERE CancellationID = @CancellationID", new { CancellationID = cancellationId });
        }

        public async Task<int> AddAsync(Cancellation cancellation)
        {
            using var conn = _db.GetConnection();
            var sql = @"INSERT INTO Cancellation (BookingID, Reason, RefundAmount, CancelDate)
                        VALUES (@BookingID, @Reason, @RefundAmount, @CancelDate);
                        SELECT LAST_INSERT_ID();";
            return await conn.ExecuteScalarAsync<int>(sql, cancellation);
        }

        public async Task<Cancellation?> GetByBookingIdAsync(int bookingId)
        {
            using var conn = _db.GetConnection();
            return await conn.QueryFirstOrDefaultAsync<Cancellation>(
                "SELECT * FROM Cancellation WHERE BookingID = @BookingID", new { BookingID = bookingId });
        }
    }
}
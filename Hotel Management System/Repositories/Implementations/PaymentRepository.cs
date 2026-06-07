using Dapper;
using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Repositories.Interfaces;

namespace HotelManagementSystem.Repositories.Implementations
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DbConnection _db;
        public PaymentRepository(DbConnection db) { _db = db; }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            using var conn = _db.GetConnection();
            return await conn.QueryAsync<Payment>("SELECT * FROM Payment");
        }

        public async Task<Payment?> GetByIdAsync(int paymentId)
        {
            using var conn = _db.GetConnection();
            return await conn.QueryFirstOrDefaultAsync<Payment>(
                "SELECT * FROM Payment WHERE PaymentID = @PaymentID", new { PaymentID = paymentId });
        }

        public async Task<int> AddAsync(Payment payment)
        {
            using var conn = _db.GetConnection();
            var sql = @"INSERT INTO Payment (BookingID, Amount, PaymentDate, Method, Status)
                        VALUES (@BookingID, @Amount, @PaymentDate, @Method, @Status);
                        SELECT LAST_INSERT_ID();";
            return await conn.ExecuteScalarAsync<int>(sql, payment);
        }

        public async Task<IEnumerable<Payment>> GetByBookingIdAsync(int bookingId)
        {
            using var conn = _db.GetConnection();
            return await conn.QueryAsync<Payment>(
                "SELECT * FROM Payment WHERE BookingID = @BookingID", new { BookingID = bookingId });
        }
    }
}
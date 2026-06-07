using Dapper;
using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Repositories.Interfaces;

namespace HotelManagementSystem.Repositories.Implementations
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DbConnection _db;
        public InvoiceRepository(DbConnection db) { _db = db; }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            using var conn = _db.GetConnection();
            return await conn.QueryAsync<Invoice>("SELECT * FROM Invoice");
        }

        public async Task<Invoice?> GetByIdAsync(int invoiceId)
        {
            using var conn = _db.GetConnection();
            return await conn.QueryFirstOrDefaultAsync<Invoice>(
                "SELECT * FROM Invoice WHERE InvoiceID = @InvoiceID", new { InvoiceID = invoiceId });
        }

        public async Task<int> AddAsync(Invoice invoice)
        {
            using var conn = _db.GetConnection();
            var sql = @"INSERT INTO Invoice (BookingID, TotalAmount, GeneratedDate, PaidStatus)
                        VALUES (@BookingID, @TotalAmount, @GeneratedDate, @PaidStatus);
                        SELECT LAST_INSERT_ID();";
            return await conn.ExecuteScalarAsync<int>(sql, invoice);
        }

        public async Task<bool> UpdateAsync(Invoice invoice)
        {
            using var conn = _db.GetConnection();
            var sql = @"UPDATE Invoice SET TotalAmount=@TotalAmount, GeneratedDate=@GeneratedDate,
                        PaidStatus=@PaidStatus WHERE InvoiceID=@InvoiceID";
            var rows = await conn.ExecuteAsync(sql, invoice);
            return rows > 0;
        }

        public async Task<Invoice?> GetByBookingIdAsync(int bookingId)
        {
            using var conn = _db.GetConnection();
            return await conn.QueryFirstOrDefaultAsync<Invoice>(
                "SELECT * FROM Invoice WHERE BookingID = @BookingID", new { BookingID = bookingId });
        }
    }
}

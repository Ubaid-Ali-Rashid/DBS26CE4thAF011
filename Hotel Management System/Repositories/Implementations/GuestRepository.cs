using Dapper;
using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Repositories.Interfaces;

namespace HotelManagementSystem.Repositories.Implementations
{
    public class GuestRepository : IGuestRepository
    {
        private readonly DbConnection _db;

        public GuestRepository(DbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Guest>> GetAllAsync()
        {
            using var conn = _db.GetConnection();
            return await conn.QueryAsync<Guest>("SELECT * FROM Guest");
        }

        public async Task<Guest?> GetByIdAsync(int guestId)
        {
            using var conn = _db.GetConnection();
            return await conn.QueryFirstOrDefaultAsync<Guest>(
                "SELECT * FROM Guest WHERE GuestID = @GuestID", new { GuestID = guestId });
        }

        public async Task<int> AddAsync(Guest guest)
        {
            using var conn = _db.GetConnection();
            var sql = @"INSERT INTO Guest (FullName, CNIC, Email, Phone, Address, RegisteredAt)
                        VALUES (@FullName, @CNIC, @Email, @Phone, @Address, @RegisteredAt);
                        SELECT LAST_INSERT_ID();";
            return await conn.ExecuteScalarAsync<int>(sql, guest);
        }

        public async Task<bool> UpdateAsync(Guest guest)
        {
            using var conn = _db.GetConnection();
            var sql = @"UPDATE Guest SET FullName=@FullName, CNIC=@CNIC, Email=@Email,
                        Phone=@Phone, Address=@Address
                        WHERE GuestID=@GuestID";
            var rows = await conn.ExecuteAsync(sql, guest);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int guestId)
        {
            using var conn = _db.GetConnection();
            var rows = await conn.ExecuteAsync("DELETE FROM Guest WHERE GuestID = @GuestID", new { GuestID = guestId });
            return rows > 0;
        }

        public async Task<Guest?> GetByEmailAsync(string email)
        {
            using var conn = _db.GetConnection();
            return await conn.QueryFirstOrDefaultAsync<Guest>(
                "SELECT * FROM Guest WHERE Email = @Email", new { Email = email });
        }
    }
}
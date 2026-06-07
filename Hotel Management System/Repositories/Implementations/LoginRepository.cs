using Dapper;
using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Repositories.Interfaces;

namespace HotelManagementSystem.Repositories.Implementations
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DbConnection _db;
        public LoginRepository(DbConnection db) { _db = db; }

        public async Task<Login?> GetByUsernameAsync(string username)
        {
            using var conn = _db.GetConnection();
            return await conn.QueryFirstOrDefaultAsync<Login>(
                "SELECT * FROM Login WHERE Username = @Username", new { Username = username });
        }

        public async Task<int> AddAsync(Login login)
        {
            using var conn = _db.GetConnection();
            var sql = @"INSERT INTO Login (EmployeeID, Username, PasswordHash, IsActive)
                        VALUES (@EmployeeID, @Username, @PasswordHash, @IsActive);
                        SELECT LAST_INSERT_ID();";
            return await conn.ExecuteScalarAsync<int>(sql, login);
        }

        public async Task<bool> UpdatePasswordAsync(int loginId, string newPasswordHash)
        {
            using var conn = _db.GetConnection();
            var rows = await conn.ExecuteAsync(
                "UPDATE Login SET PasswordHash=@PasswordHash WHERE LoginID=@LoginID",
                new { PasswordHash = newPasswordHash, LoginID = loginId });
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int loginId)
        {
            using var conn = _db.GetConnection();
            var rows = await conn.ExecuteAsync("DELETE FROM Login WHERE LoginID = @LoginID", new { LoginID = loginId });
            return rows > 0;
        }
    }
}

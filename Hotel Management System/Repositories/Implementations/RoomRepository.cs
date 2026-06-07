using Dapper;
using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Repositories.Interfaces;

namespace HotelManagementSystem.Repositories.Implementations
{
    public class RoomRepository : IRoomRepository
    {
        private readonly DbConnection _db;
        public RoomRepository(DbConnection db) { _db = db; }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            using var conn = _db.GetConnection();
            return await conn.QueryAsync<Room>("SELECT * FROM Room");
        }

        public async Task<Room?> GetByIdAsync(int roomId)
        {
            using var conn = _db.GetConnection();
            return await conn.QueryFirstOrDefaultAsync<Room>(
                "SELECT * FROM Room WHERE RoomID = @RoomID", new { RoomID = roomId });
        }

        public async Task<int> AddAsync(Room room)
        {
            using var conn = _db.GetConnection();
            var sql = @"INSERT INTO Room (RoomNumber, TypeID, Floor, PricePerNight, Status)
                        VALUES (@RoomNumber, @TypeID, @Floor, @PricePerNight, @Status);
                        SELECT LAST_INSERT_ID();";
            return await conn.ExecuteScalarAsync<int>(sql, room);
        }

        public async Task<bool> UpdateAsync(Room room)
        {
            using var conn = _db.GetConnection();
            var sql = @"UPDATE Room SET RoomNumber=@RoomNumber, TypeID=@TypeID,
                        Floor=@Floor, PricePerNight=@PricePerNight, Status=@Status
                        WHERE RoomID=@RoomID";
            var rows = await conn.ExecuteAsync(sql, room);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int roomId)
        {
            using var conn = _db.GetConnection();
            var rows = await conn.ExecuteAsync("DELETE FROM Room WHERE RoomID = @RoomID", new { RoomID = roomId });
            return rows > 0;
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut)
        {
            using var conn = _db.GetConnection();
            var sql = @"SELECT * FROM Room WHERE RoomID NOT IN (
                            SELECT RoomID FROM Booking
                            WHERE Status NOT IN ('Cancelled', 'CheckedOut')
                            AND CheckIn < @CheckOut AND CheckOut > @CheckIn)";
            return await conn.QueryAsync<Room>(sql, new { CheckIn = checkIn, CheckOut = checkOut });
        }
    }
}
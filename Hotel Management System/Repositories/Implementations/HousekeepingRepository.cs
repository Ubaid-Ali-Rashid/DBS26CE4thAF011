using Dapper;
using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Repositories.Interfaces;

namespace HotelManagementSystem.Repositories.Implementations
{
    public class HousekeepingRepository : IHousekeepingRepository
    {
        private readonly DbConnection _db;
        public HousekeepingRepository(DbConnection db) { _db = db; }

        public async Task<IEnumerable<Housekeeping>> GetAllAsync()
        {
            using var conn = _db.GetConnection();
            return await conn.QueryAsync<Housekeeping>("SELECT * FROM Housekeeping");
        }

        public async Task<Housekeeping?> GetByIdAsync(int taskId)
        {
            using var conn = _db.GetConnection();
            return await conn.QueryFirstOrDefaultAsync<Housekeeping>(
                "SELECT * FROM Housekeeping WHERE TaskID = @TaskID", new { TaskID = taskId });
        }

        public async Task<int> AddAsync(Housekeeping housekeeping)
        {
            using var conn = _db.GetConnection();
            var sql = @"INSERT INTO Housekeeping (RoomID, EmployeeID, ScheduledDate, Status, Notes)
                        VALUES (@RoomID, @EmployeeID, @ScheduledDate, @Status, @Notes);
                        SELECT LAST_INSERT_ID();";
            return await conn.ExecuteScalarAsync<int>(sql, housekeeping);
        }

        public async Task<bool> UpdateAsync(Housekeeping housekeeping)
        {
            using var conn = _db.GetConnection();
            var sql = @"UPDATE Housekeeping SET RoomID=@RoomID, EmployeeID=@EmployeeID,
                        ScheduledDate=@ScheduledDate, Status=@Status, Notes=@Notes
                        WHERE TaskID=@TaskID";
            var rows = await conn.ExecuteAsync(sql, housekeeping);
            return rows > 0;
        }

        public async Task<IEnumerable<Housekeeping>> GetByRoomIdAsync(int roomId)
        {
            using var conn = _db.GetConnection();
            return await conn.QueryAsync<Housekeeping>(
                "SELECT * FROM Housekeeping WHERE RoomID = @RoomID", new { RoomID = roomId });
        }
    }
}
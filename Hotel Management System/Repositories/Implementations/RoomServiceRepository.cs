using Dapper;
using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Repositories.Interfaces;

namespace HotelManagementSystem.Repositories.Implementations
{
    public class RoomServiceRepository : IRoomServiceRepository
    {
        private readonly DbConnection _db;
        public RoomServiceRepository(DbConnection db) { _db = db; }

        public async Task<IEnumerable<RoomService>> GetAllAsync()
        {
            using var conn = _db.GetConnection();
            return await conn.QueryAsync<RoomService>("SELECT * FROM RoomService");
        }

        public async Task<RoomService?> GetByIdAsync(int serviceId)
        {
            using var conn = _db.GetConnection();
            return await conn.QueryFirstOrDefaultAsync<RoomService>(
                "SELECT * FROM RoomService WHERE ServiceID = @ServiceID", new { ServiceID = serviceId });
        }

        public async Task<int> AddAsync(RoomService roomService)
        {
            using var conn = _db.GetConnection();
            var sql = @"INSERT INTO RoomService (BookingID, ServiceTypeID, Quantity, RequestTime, Status)
                        VALUES (@BookingID, @ServiceTypeID, @Quantity, @RequestTime, @Status);
                        SELECT LAST_INSERT_ID();";
            return await conn.ExecuteScalarAsync<int>(sql, roomService);
        }

        public async Task<bool> UpdateAsync(RoomService roomService)
        {
            using var conn = _db.GetConnection();
            var sql = @"UPDATE RoomService SET BookingID=@BookingID, ServiceTypeID=@ServiceTypeID,
                        Quantity=@Quantity, RequestTime=@RequestTime, Status=@Status
                        WHERE ServiceID=@ServiceID";
            var rows = await conn.ExecuteAsync(sql, roomService);
            return rows > 0;
        }

        public async Task<IEnumerable<RoomService>> GetByBookingIdAsync(int bookingId)
        {
            using var conn = _db.GetConnection();
            return await conn.QueryAsync<RoomService>(
                "SELECT * FROM RoomService WHERE BookingID = @BookingID", new { BookingID = bookingId });
        }
    }
}
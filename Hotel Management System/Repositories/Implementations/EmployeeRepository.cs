using Dapper;
using HotelManagementSystem.Data;
using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Repositories.Interfaces;

namespace HotelManagementSystem.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbConnection _db;
        public EmployeeRepository(DbConnection db) { _db = db; }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            using var conn = _db.GetConnection();
            return await conn.QueryAsync<Employee>("SELECT * FROM Employee");
        }

        public async Task<Employee?> GetByIdAsync(int employeeId)
        {
            using var conn = _db.GetConnection();
            return await conn.QueryFirstOrDefaultAsync<Employee>(
                "SELECT * FROM Employee WHERE EmployeeID = @EmployeeID", new { EmployeeID = employeeId });
        }

        public async Task<int> AddAsync(Employee employee)
        {
            using var conn = _db.GetConnection();
            var sql = @"INSERT INTO Employee (FullName, CNIC, Phone, Role, Salary, HireDate)
                        VALUES (@FullName, @CNIC, @Phone, @Role, @Salary, @HireDate);
                        SELECT LAST_INSERT_ID();";
            return await conn.ExecuteScalarAsync<int>(sql, employee);
        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
            using var conn = _db.GetConnection();
            var sql = @"UPDATE Employee SET FullName=@FullName, CNIC=@CNIC, Phone=@Phone,
                        Role=@Role, Salary=@Salary, HireDate=@HireDate
                        WHERE EmployeeID=@EmployeeID";
            var rows = await conn.ExecuteAsync(sql, employee);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int employeeId)
        {
            using var conn = _db.GetConnection();
            var rows = await conn.ExecuteAsync("DELETE FROM Employee WHERE EmployeeID = @EmployeeID", new { EmployeeID = employeeId });
            return rows > 0;
        }
    }
}
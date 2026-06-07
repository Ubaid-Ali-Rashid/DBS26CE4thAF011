using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Repositories.Interfaces;
using HotelManagementSystem.Services.Interfaces;

namespace HotelManagementSystem.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;
        public EmployeeService(IEmployeeRepository repo) { _repo = repo; }

        public Task<IEnumerable<Employee>> GetAllEmployeesAsync() => _repo.GetAllAsync();
        public Task<Employee?> GetEmployeeByIdAsync(int employeeId) => _repo.GetByIdAsync(employeeId);
        public Task<int> AddEmployeeAsync(Employee employee) => _repo.AddAsync(employee);
        public Task<bool> UpdateEmployeeAsync(Employee employee) => _repo.UpdateAsync(employee);
        public Task<bool> DeleteEmployeeAsync(int employeeId) => _repo.DeleteAsync(employeeId);
    }
}
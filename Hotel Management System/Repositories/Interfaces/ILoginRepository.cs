using HotelManagementSystem.Models.Domain;

namespace HotelManagementSystem.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Task<Login?> GetByUsernameAsync(string username);
        Task<int> AddAsync(Login login);
        Task<bool> UpdatePasswordAsync(int loginId, string newPasswordHash);
        Task<bool> DeleteAsync(int loginId);
    }
}
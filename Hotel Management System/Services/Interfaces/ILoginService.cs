using HotelManagementSystem.Models.Domain;

namespace HotelManagementSystem.Services.Interfaces
{
    public interface ILoginService
    {
        Task<Login?> GetByUsernameAsync(string username);
        Task<int> AddLoginAsync(Login login);
        Task<bool> UpdatePasswordAsync(int loginId, string newPasswordHash);
        Task<bool> DeleteLoginAsync(int loginId);
        Task<bool> ValidateLoginAsync(string username, string passwordHash);
    }
}
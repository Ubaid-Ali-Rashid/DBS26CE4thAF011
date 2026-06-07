using HotelManagementSystem.Models.Domain;
using HotelManagementSystem.Repositories.Interfaces;
using HotelManagementSystem.Services.Interfaces;

namespace HotelManagementSystem.Services.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _repo;
        public LoginService(ILoginRepository repo) { _repo = repo; }

        public Task<Login?> GetByUsernameAsync(string username) => _repo.GetByUsernameAsync(username);
        public Task<int> AddLoginAsync(Login login) => _repo.AddAsync(login);
        public Task<bool> UpdatePasswordAsync(int loginId, string newPasswordHash) =>
            _repo.UpdatePasswordAsync(loginId, newPasswordHash);
        public Task<bool> DeleteLoginAsync(int loginId) => _repo.DeleteAsync(loginId);

        public async Task<bool> ValidateLoginAsync(string username, string passwordHash)
        {
            var login = await _repo.GetByUsernameAsync(username);
            return login != null && login.PasswordHash == passwordHash && login.IsActive;
        }
    }
}
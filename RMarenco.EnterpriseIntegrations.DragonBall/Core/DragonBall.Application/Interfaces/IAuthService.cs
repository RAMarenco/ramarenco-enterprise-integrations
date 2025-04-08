using DragonBall.Domain.Entities;

namespace DragonBall.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> AddUser(string email, string password, string confirmPassword);
        Task<string?> LogIn(string email, string password);
    }
}

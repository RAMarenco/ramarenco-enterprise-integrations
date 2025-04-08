using DragonBall.Domain.Entities;

namespace DragonBall.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(Guid id);
    }
}

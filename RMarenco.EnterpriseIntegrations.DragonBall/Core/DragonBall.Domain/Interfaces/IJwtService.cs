using DragonBall.Domain.Entities;

namespace DragonBall.Domain.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        Task<bool> ValidateTokenBelongsToUserAsync(string token, string userId);
    }
}

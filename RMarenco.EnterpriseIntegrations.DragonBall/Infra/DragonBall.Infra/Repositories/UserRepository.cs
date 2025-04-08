using DragonBall.Domain.Entities;
using DragonBall.Domain.Interfaces;
using DragonBall.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DragonBall.Infra.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        public async Task AddUser(User user)
        {
            if (context.Users.Any(u => u.Id == user.Id))
            {
                context.Users.Update(user);
            } 
            else
            {
                await context.Users.AddAsync(user).ConfigureAwait(false);
            }

            await context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await context.Set<User>()
                .AsNoTracking()
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await context.Set<User>()
                .AsNoTracking()
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }
    }
}

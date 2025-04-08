using DragonBall.Domain.Entities;
using DragonBall.Domain.Interfaces;
using DragonBall.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DragonBall.Infra.Repositories
{
    public class CharacterRepository(AppDbContext context) : ICharacterRepository
    {
        public async Task AddCharacter(Character character)
        {
            await context.Characters.AddAsync(character).ConfigureAwait(false);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Character>> GetAllCharacters()
        {
            return await context.Set<Character>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Character>> GetAllCharactersWithTransformations()
        {
            return await context.Set<Character>()
                .Include(c => c.Transformation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Character>> GetCharacterByAffiliation(string affiliation)
        {
            return await context.Set<Character>()
                .AsNoTracking()
                .Where(c => c.Affiliation == affiliation)
                .ToListAsync();
        }

        public async Task<IEnumerable<Character>> GetCharacterByAffiliationWithTransformations(string affiliation)
        {
            return await context.Set<Character>()
                .Include(c => c.Transformation)
                .AsNoTracking()
                .Where(c => c.Affiliation == affiliation)
                .ToListAsync();
        }

        public async Task<Character?> GetCharacterById(int id)
        {
            return await context.Set<Character>()
                .AsNoTracking()
                .Where(c => c.ExternalId == id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<Character?> GetCharacterByIdWithTransformations(int id)
        {
            return await context.Set<Character>()
                .Include(c => c.Transformation)
                .AsNoTracking()
                .Where(c => c.ExternalId == id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Character>> GetCharacterByName(string name)
        {
            return await context.Set<Character>()
                .AsNoTracking()
                .Where(c => EF.Functions.Like(c.Name, $"%{name}%"))
                .ToListAsync();
        }

        public async Task<IEnumerable<Character>> GetCharacterByNameWithTransformations(string name)
        {
            return await context.Set<Character>()
                .Include(c => c.Transformation)
                .AsNoTracking()
                .Where(c => EF.Functions.Like(c.Name, $"%{name}%"))
                .ToListAsync();
        }
    }
}

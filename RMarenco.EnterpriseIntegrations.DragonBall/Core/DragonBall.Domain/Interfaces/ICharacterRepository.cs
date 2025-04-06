using DragonBall.Domain.Entities;

namespace DragonBall.Domain.Interfaces
{
    public interface ICharacterRepository
    {
        Task AddCharacter(Character character);
        Task<IEnumerable<Character>> GetAllCharacters();
        Task<IEnumerable<Character>> GetAllCharactersWithTransformations();
        Task<Character?> GetCharacterById(int id);
        Task<Character?> GetCharacterByIdWithTransformations(int id);
        Task<IEnumerable<Character>> GetCharacterByName(string name);
        Task<IEnumerable<Character>> GetCharacterByNameWithTransformations(string name);
        Task<IEnumerable<Character>> GetCharacterByAffiliation(string affiliation);
        Task<IEnumerable<Character>> GetCharacterByAffiliationWithTransformations(string affiliation);
    }
}

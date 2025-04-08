using DragonBall.Application.DTOs;

namespace DragonBall.Application.Interfaces
{
    public interface ICharacterService
    {
        Task AddCharacters();
        Task<IEnumerable<CharacterDto>> GetAllCharacters(bool includeTransformations);
        Task<CharacterDto?> GetCharacterById(int id, bool includeTransformations);
        Task<IEnumerable<CharacterDto>> GetCharacterByName(string name, bool includeTransformations);
        Task<IEnumerable<CharacterDto>> GetCharacterByAffiliation(string affiliation, bool includeTransformations);
    }
}

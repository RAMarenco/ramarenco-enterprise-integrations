using DragonBall.Domain.Models;

namespace DragonBall.Domain.Interfaces.ExternalServices
{
    public interface IDragonBallApi
    {
        Task<CharacterResponse?> GetDragonBallCharacters(int page, int limit, string race, string affiliation);
    }
}
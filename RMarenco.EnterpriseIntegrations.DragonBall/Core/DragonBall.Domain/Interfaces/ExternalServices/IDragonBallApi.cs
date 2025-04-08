using DragonBall.Domain.Models;

namespace DragonBall.Domain.Interfaces.ExternalServices
{
    public interface IDragonBallApi
    {
        Task<List<CharacterResponse>?> GetDragonBallCharacters(string race);
        Task<CharacterTransformationResponse?> GetDragonBallTransformations(int id);
    }
}
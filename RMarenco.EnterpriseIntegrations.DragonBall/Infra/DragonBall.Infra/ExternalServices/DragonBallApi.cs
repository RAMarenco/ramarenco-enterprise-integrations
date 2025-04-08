using DragonBall.Domain.Interfaces.ExternalServices;
using DragonBall.Domain.Models;
using System.Net.Http.Json;

namespace DragonBall.Infra.ExternalServices
{
    public class DragonBallApi(HttpClient httpClient) : IDragonBallApi
    {
        public async Task<List<CharacterResponse>?> GetDragonBallCharacters(string race = "")
        {
            return await httpClient.GetFromJsonAsync<List<CharacterResponse>>
                ($"characters?race={race}");
        }

        public async Task<CharacterTransformationResponse?> GetDragonBallTransformations(int id = 1)
        {
            return await httpClient.GetFromJsonAsync<CharacterTransformationResponse>
                ($"characters/{id}");
        }
    }
}

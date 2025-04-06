using DragonBall.Domain.Interfaces.ExternalServices;
using DragonBall.Domain.Models;
using System.Net.Http.Json;

namespace DragonBall.Infra.ExternalServices
{
    public class DragonBallApi(HttpClient httpClient) : IDragonBallApi
    {
        public async Task<CharacterResponse?> GetDragonBallCharacters(int page = 1, int limit = 10, string race = "", string affiliation = "")
        {
            return await httpClient.GetFromJsonAsync<CharacterResponse>
                ($"characters?page={page}&limit={limit}&race={race}&affiliation={affiliation}");
        }

        public async Task<TransformationResponse?> GetDragonBallTransformations()
        {
            return await httpClient.GetFromJsonAsync<TransformationResponse>
                ($"transformations");
        }
    }
}

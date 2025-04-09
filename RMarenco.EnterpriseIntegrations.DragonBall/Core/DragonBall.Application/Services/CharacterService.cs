using DragonBall.Application.CustomExceptions;
using DragonBall.Application.DTOs;
using DragonBall.Application.Interfaces;
using DragonBall.Application.Mapping;
using DragonBall.Domain.Interfaces;
using DragonBall.Domain.Interfaces.ExternalServices;
using System.Diagnostics;

namespace DragonBall.Application.Services
{
    public class CharacterService(ICharacterRepository characterRepository, ITransformationService transformationService,IDragonBallApi dragonBallApiClient) : ICharacterService
    {
        public async Task AddCharacters()
        {
            var existingCharacters = await characterRepository.GetAllCharacters().ConfigureAwait(false);
            if (existingCharacters is not null && existingCharacters.Any())
            {
                throw new ConflictException($"In order to sync the database, please remove the existing characters.");
            }

            string race = "Saiyan";

            var characters = await dragonBallApiClient
                .GetDragonBallCharacters(race)
                .ConfigureAwait(false) ?? throw new NoContentException("No data available");

            foreach (var character in characters)
            {
                var characterDb = character.ToCharacterDto().ToCharacter();
                await characterRepository.AddCharacter(characterDb).ConfigureAwait(false);

                if (characterDb.Affiliation != "Z Fighter")
                {
                    continue;
                }

                await transformationService.AddTransformations(characterDb)
                    .ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<CharacterDto>> GetAllCharacters(bool includeTransformations)
        {
            var characterDb = includeTransformations ?
                    await characterRepository.GetAllCharactersWithTransformations().ConfigureAwait(false) :
                    await characterRepository.GetAllCharacters().ConfigureAwait(false);

            var characterDtos = characterDb.Select(character => character.ToCharacterDto()).ToList();
            return characterDtos;
        }

        public async Task<IEnumerable<CharacterDto>> GetCharacterByAffiliation(string affiliation, bool includeTransformations)
        {
            var characterDb = includeTransformations ?
                    await characterRepository.GetCharacterByAffiliationWithTransformations(affiliation).ConfigureAwait(false) :
                    await characterRepository.GetCharacterByAffiliation(affiliation).ConfigureAwait(false);

            var characterDtos = characterDb.Select(character => character.ToCharacterDto()).ToList();
            return characterDtos;
        }

        public async Task<CharacterDto?> GetCharacterById(int id, bool includeTransformations)
        {
            var characterDb = includeTransformations ?
                    await characterRepository.GetCharacterByIdWithTransformations(id).ConfigureAwait(false) :
                    await characterRepository.GetCharacterById(id).ConfigureAwait(false);

            return characterDb is null ? throw new NotFoundException($"Character with id {id} was not found.") : characterDb.ToCharacterDto();
        }

        public async Task<IEnumerable<CharacterDto>> GetCharacterByName(string name, bool includeTransformations)
        {
            var characterDb = includeTransformations ?
                    await characterRepository.GetCharacterByNameWithTransformations(name).ConfigureAwait(false) :
                    await characterRepository.GetCharacterByName(name).ConfigureAwait(false);

            var characterDtos = characterDb.Select(character => character.ToCharacterDto()).ToList();
            return characterDtos;
        }
    }
}

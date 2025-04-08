using DragonBall.Application.DTOs;
using DragonBall.Domain.Entities;

namespace DragonBall.Application.Mapping
{
    public static class CharacterMapping
    {
        public static Character ToCharacter(this CharacterDto characterDto)
        {
            var character = new Character
            {
                ExternalId = characterDto.Id,
                Name = characterDto.Name,
                Ki = characterDto.Ki,
                Race = characterDto.Race,
                Gender = characterDto.Gender,
                Description = characterDto.Description,
                Affiliation = characterDto.Affiliation,
            };
            return character;
        }

        public static CharacterDto ToCharacterDto(this Character character)
        {
            var characterDto = new CharacterDto
            {
                Id = character.ExternalId,
                Name = character.Name,
                Ki = character.Ki,
                Race = character.Race,
                Gender = character  .Gender,
                Description = character.Description,
                Affiliation = character.Affiliation,
                Transformation = character.Transformation?.Select(transformation => transformation.ToTransformationDto()).ToList() ?? null,
            };
            return characterDto;
        }

        public static CharacterDto ToCharacterDto(this DragonBall.Domain.Models.CharacterResponse character)
        {
            var characterDto = new CharacterDto
            {
                Id = character.Id,
                Name = character.Name,
                Ki = character.Ki,
                Race = character.Race,
                Gender = character.Gender,
                Description = character.Description,
                Affiliation = character.Affiliation,
            };
            return characterDto;
        }
    }
}

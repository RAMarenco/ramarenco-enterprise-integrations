using DragonBall.Application.Interfaces;
using DragonBall.Domain.Interfaces.ExternalServices;
using DragonBall.Domain.Interfaces;
using DragonBall.Domain.Entities;
using DragonBall.Application.Mapping;
using DragonBall.Application.DTOs;

namespace DragonBall.Application.Services
{
    public class TransformationService(ITransformationRepository transformationRepository, IDragonBallApi dragonBallApiClient) : ITransformationService
    {
        public async Task AddTransformations(Character character)
        {
            var transformations = await dragonBallApiClient
                .GetDragonBallTransformations(character.ExternalId)
                .ConfigureAwait(false);

            if (transformations is not null)
            {
                List<Transformation> transformationsDb = [.. transformations.Transformations
                    .Select(transformation => transformation
                        .ToTransformationDto().ToTransformation())];

                foreach (var transformation in transformationsDb)
                {
                    transformation.CharacterId = character.Id;
                }
                await transformationRepository.AddTransformations(transformationsDb).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<TransformationDto>> GetAllTransformations()
        {
            var transformationDb = await transformationRepository.GetAllTransformations().ConfigureAwait(false);

            var transformationsDtos = transformationDb.Select(transformation => transformation.ToTransformationDto()).ToList();
            return transformationsDtos;
        }
    }
}

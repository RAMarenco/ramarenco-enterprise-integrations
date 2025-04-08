using DragonBall.Application.DTOs;
using DragonBall.Domain.Entities;

namespace DragonBall.Application.Mapping
{
    public static class TransformationMapping
    {
        public static Transformation ToTransformation(this TransformationDto transformationDto)
        {
            var transformation = new Transformation
            {
                ExternalId = transformationDto.Id,
                Name = transformationDto.Name,
                Ki = transformationDto.Ki,
            };
            return transformation;
        }

        public static TransformationDto ToTransformationDto(this Transformation transformation)
        {
            var transformationDto = new TransformationDto
            {
                Id = transformation.ExternalId,
                Name = transformation.Name,
                Ki = transformation.Ki,
            };
            return transformationDto;
        }

        public static TransformationDto ToTransformationDto(this DragonBall.Domain.Models.Transformation transformation)
        {
            var transformationDto = new TransformationDto
            {
                Id = transformation.Id,
                Name = transformation.Name,
                Ki = transformation.Ki,
            };
            return transformationDto;
        }
    }
}

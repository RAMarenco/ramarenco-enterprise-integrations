using DragonBall.Domain.Entities;
using System.Text.Json.Serialization;

namespace DragonBall.Application.DTOs
{
    public class CharacterDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Ki { get; set; }
        public required string Race { get; set; }
        public required string Gender { get; set; }
        public required string Description { get; set; }
        public required string Affiliation { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<TransformationDto>? Transformation { get; set; }
    }
}

namespace DragonBall.Domain.Entities
{
    public class Transformation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int ExternalId { get; set; }
        public string Name { get; set; }
        public string Ki { get; set; }

        public Guid CharacterId { get; set; }
    }
}

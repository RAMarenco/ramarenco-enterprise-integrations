namespace DragonBall.Domain.Entities
{
    public class Character
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int ExternalId { get; set; }
        public string Name { get; set; }
        public string Ki { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public string Affiliation { get; set; }

        public List<Transformation> Transformation { get; set; }
    }
}

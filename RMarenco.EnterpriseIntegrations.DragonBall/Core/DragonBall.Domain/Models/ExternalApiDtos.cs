namespace DragonBall.Domain.Models
{
    public class CharacterResponse
    {
        public List<Character> Characters { get; set; }
        public Meta Meta { get; set; }
        public Links Links { get; set; }
    }

    public class TransformationResponse
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ki { get; set; }
        public string DeletedAt { get; set; }
    }

    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ki { get; set; }
        public string MaxKi { get; set; }
        public string Race { get; set; }
        public string Gener { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Affiliation { get; set; }
        public string DeletedAt { get; set; }
    }

    public class Meta
    {
        public int Totalitems { get; set; }
        public int ItemCount { get; set; }
        public int ItemsPerPage { get; set; }
        public int totalPages { get; set; }
        public int currentPage { get; set; }
    }

    public class Links
    {
        public string First { get; set; }
        public string Previous { get; set; }
        public string Next { get; set; }
        public string Last { get; set; }
    }
}
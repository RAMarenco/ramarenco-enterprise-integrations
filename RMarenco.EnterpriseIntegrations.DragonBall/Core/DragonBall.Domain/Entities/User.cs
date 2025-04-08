namespace DragonBall.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string AccessToken { get; set; }
    }
}

namespace DragonBall.Domain.Interfaces
{
    public interface ICryptographyService
    {
        (string hashedPassword, string salt) HashPassword(string password);
        bool VerifyPassword(string password, string storedHash, string storedSalt);
    }
}

using DragonBall.Domain.Interfaces;
using System.Security.Cryptography;

namespace DragonBall.Infra.Services
{
    class CryptographyService : ICryptographyService
    {
        public (string hashedPassword, string salt) HashPassword(string password)
        {
            var salt = GenerateSalt();
            var hashedPassword = HashPasswordWithSalt(password, salt);
            return (hashedPassword, salt);
        }

        public bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            var hashedPassword = HashPasswordWithSalt(password, storedSalt);
            return storedHash == hashedPassword;
        }

        private static string GenerateSalt()
        {
            var saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private static string HashPasswordWithSalt(string password, string salt)
        {
            using Rfc2898DeriveBytes pbkdf2 = new(password, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA256);
            byte[] hashBytes = pbkdf2.GetBytes(32); // 256-bit hash
            return Convert.ToBase64String(hashBytes);
        }
    }
}

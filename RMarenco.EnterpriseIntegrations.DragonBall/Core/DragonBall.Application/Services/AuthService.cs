using DragonBall.Application.CustomExceptions;
using DragonBall.Application.Interfaces;
using DragonBall.Domain.Entities;
using DragonBall.Domain.Interfaces;

namespace DragonBall.Application.Services
{
    class AuthService(IUserRepository userRepository, IJwtService jwtService, ICryptographyService cryptographyService) : IAuthService
    {
        public async Task<string> AddUser(string email, string password, string confirmPassword)
        {
            // Check if password and confirm password match
            if (password != confirmPassword)
            {
                throw new BadRequestException("Password and confirmation must be identical.");
            }

            // Check if password meets the complexity requirements
            if (!IsValidPassword(password))
            {
                throw new BadRequestException("Password must be at least 8 characters long, contain 1 uppercase letter, 1 lowercase letter, 1 number, and 1 special symbol.");
            }

            User? userDb = await userRepository.GetUserByEmail(email).ConfigureAwait(false);

            if (userDb is not null)
            {
                throw new ConflictException($"User with email {email} already exists.");
            }

            var (hashedPassword, salt) = cryptographyService.HashPassword(password);

            User newUser = new()
            {
                Email = email,
                Password = hashedPassword,
                Salt = salt
            };

            string jwt = jwtService.GenerateToken(newUser);
            newUser.AccessToken = jwt;

            await userRepository.AddUser(newUser).ConfigureAwait(false);

            return jwt;
        }

        public async Task<string?> LogIn(string email, string password)
        {
            User? userDb = await userRepository.GetUserByEmail(email).ConfigureAwait(false) ?? throw new BadRequestException($"Invalid Credentials");
            if (!cryptographyService.VerifyPassword(password, userDb.Password, userDb.Salt))
            {
                throw new BadRequestException($"Invalid Credentials");
            }

            string jwt = jwtService.GenerateToken(userDb);
            userDb.AccessToken = jwt;
            await userRepository.AddUser(userDb).ConfigureAwait(false);
            return jwt;
        }

        private static bool IsValidPassword(string password)
        {
            // Check for minimum length
            if (password.Length < 8)
                return false;

            // Regular expression to check for uppercase, lowercase, number, and symbol
            var hasUpperCase = password.Any(char.IsUpper);
            var hasLowerCase = password.Any(char.IsLower);
            var hasNumber = password.Any(char.IsDigit);
            var hasSymbol = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpperCase && hasLowerCase && hasNumber && hasSymbol;
        }
    }
}

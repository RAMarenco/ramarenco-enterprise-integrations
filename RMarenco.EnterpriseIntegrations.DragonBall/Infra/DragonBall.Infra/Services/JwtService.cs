using DragonBall.Domain.Entities;
using DragonBall.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DragonBall.Infra.Services
{
    public class JwtService(IOptions<JwtSettings> options, IUserRepository userRepository) : IJwtService
    {
        private readonly JwtSettings _settings = options.Value;

        public async Task<bool> ValidateTokenBelongsToUserAsync(string token, string userId)
        {
            try
            {
                // 1. Basic JWT validation
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _settings.Issuer,
                    ValidAudience = _settings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_settings.Secret))
                };

                // This validates signature, issuer, audience, and expiry
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                // 2. Verify token belongs to the user
                var tokenUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
                if (tokenUserId != userId)
                {
                    return false;
                }

                // 3. Check against database
                User? user = await userRepository.GetUserById(Guid.Parse(userId));
                if (user?.AccessToken != token)
                {
                    return false;
                }

                return true;
            }
            catch (SecurityTokenException ex)
            {
                return false;
            }
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.UtcNow.AddMinutes(_settings.ExpirationInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _settings.Issuer,
                ValidAudience = _settings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_settings.Secret))
            };
        }
    }

    public class JwtSettings
    {
        public string Secret { get; set; }
        public int ExpirationInMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}

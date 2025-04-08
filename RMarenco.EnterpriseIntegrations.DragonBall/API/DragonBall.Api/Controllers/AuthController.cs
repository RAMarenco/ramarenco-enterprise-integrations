using DragonBall.Application.DTOs.SwaggerResponses;
using DragonBall.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DragonBall.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(Response), 400)]
        public async Task<IActionResult> Login([FromBody] LogInBody logInBody)
        {
            return Ok($"{await authService.LogIn(logInBody.Email, logInBody.Password)}");
        }

        [HttpPost]
        [Route("signin")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(Response), 400)]
        [ProducesResponseType(typeof(Response), 409)]
        public async Task<IActionResult> SignIn([FromBody] SignInBody signInBody)
        {
            return Ok($"{await authService.AddUser(
                signInBody.Email, 
                signInBody.Password, 
                signInBody.ConfirmPassword)}");
        }

        public record LogInBody
        {
            [Required]
            [EmailAddress]
            [DefaultValue("secureuser@dragonballapi.com")]
            public required string Email { get; set; }
            [Required]
            [DefaultValue("SecurePassword_12")]
            public required string Password { get; set; }
        }

        public record SignInBody
        {
            [Required]
            [EmailAddress]
            [DefaultValue("secureuser@dragonballapi.com")]
            public required string Email { get; set; }
            [Required]
            [DefaultValue("SecurePassword_12")]
            public required string Password { get; set; }
            [Required]
            [DefaultValue("WrongConfirm_12")]
            public required string ConfirmPassword { get; set; }
        }
    }
}

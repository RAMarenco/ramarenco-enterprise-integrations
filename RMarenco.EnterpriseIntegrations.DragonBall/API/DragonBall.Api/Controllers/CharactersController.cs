using DragonBall.Application.DTOs;
using DragonBall.Application.DTOs.SwaggerResponses;
using DragonBall.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DragonBall.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController(ICharacterService characterService) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CharacterDto), 200)]
        public async Task<IActionResult> GetCharacters([FromQuery] bool includeTransformations = false)
        {
            return Ok(await characterService.GetAllCharacters(includeTransformations));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CharacterDto), 200)]
        public async Task<IActionResult> GetCharacterById([FromRoute] int id, [FromQuery] bool includeTransformations = false)
        {
            return Ok(await characterService.GetCharacterById(id, includeTransformations));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{name}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CharacterDto), 200)]
        public async Task<IActionResult> GetCharacterByName([FromRoute] string name, [FromQuery] bool includeTransformations = false)
        {
            return Ok(await characterService.GetCharacterByName(name, includeTransformations));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("affiliation/{affiliation}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CharacterDto), 200)]
        public async Task<IActionResult> GetCharacterByAffiliation([FromRoute] string affiliation, [FromQuery] bool includeTransformations = false)
        {
            return Ok(await characterService.GetCharacterByAffiliation(affiliation, includeTransformations));
        }

        [HttpPost]
        [Route("sync")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(Response), 409)]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> SyncCharacters()
        {
            await characterService.AddCharacters();
            return Ok("Database syncronized successfully");
        }
    }
}

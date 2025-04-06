using Microsoft.AspNetCore.Mvc;

namespace DragonBall.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCharacters()
        {
            return Ok();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCharacterById()
        {
            return Ok();
        }

        [HttpPost]
        [Route("sync")]
        public async Task<IActionResult> SyncCharacters()
        {
            return Ok();
        }
    }
}

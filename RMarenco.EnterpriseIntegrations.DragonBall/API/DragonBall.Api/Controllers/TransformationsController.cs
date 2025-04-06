using Microsoft.AspNetCore.Mvc;

namespace DragonBall.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransformationsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTransformations()
        {
            return Ok();
        }
    }
}

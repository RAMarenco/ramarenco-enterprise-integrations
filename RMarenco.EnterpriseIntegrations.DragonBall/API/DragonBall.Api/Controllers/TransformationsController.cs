using DragonBall.Application.DTOs;
using DragonBall.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DragonBall.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransformationsController(ITransformationService transformationService) : ControllerBase
    {
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TransformationDto), 200)]
        public async Task<IActionResult> GetTransformations()
        {
            return Ok(await transformationService.GetAllTransformations());
        }
    }
}

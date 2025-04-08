using DragonBall.Application.DTOs;
using DragonBall.Domain.Entities;

namespace DragonBall.Application.Interfaces
{
    public interface ITransformationService
    {
        Task AddTransformations(Character character);
        Task<IEnumerable<TransformationDto>> GetAllTransformations();
    }
}

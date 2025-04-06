using DragonBall.Domain.Entities;

namespace DragonBall.Domain.Interfaces
{
    public interface ITransformationRepository
    {
        Task AddTransformation(Transformation transformation);
        Task<IEnumerable<Transformation>> GetAllTransformations();
    }
}

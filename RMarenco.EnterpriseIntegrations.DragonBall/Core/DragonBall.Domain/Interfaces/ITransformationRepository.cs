using DragonBall.Domain.Entities;

namespace DragonBall.Domain.Interfaces
{
    public interface ITransformationRepository
    {
        Task AddTransformations(IEnumerable<Transformation> transformation);
        Task<IEnumerable<Transformation>> GetAllTransformations();
    }
}

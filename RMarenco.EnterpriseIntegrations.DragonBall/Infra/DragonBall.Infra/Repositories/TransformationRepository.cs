using DragonBall.Domain.Entities;
using DragonBall.Domain.Interfaces;
using DragonBall.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DragonBall.Infra.Repositories
{
    class TransformationRepository(AppDbContext context) : ITransformationRepository
    {
        public async Task AddTransformations(IEnumerable<Transformation> transformation)
        {
            await context.Transformations.AddRangeAsync(transformation).ConfigureAwait(false);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transformation>> GetAllTransformations()
        {
            return await context.Set<Transformation>()
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

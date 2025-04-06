using DragonBall.Domain.Interfaces;
using DragonBall.Infra.Persistence;
using DragonBall.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DragonBall.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DragonBallDbConnection"),
                    opt => opt.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
            });

            services.AddTransient<ICharacterRepository, CharacterRepository>();
            services.AddTransient<ITransformationRepository, TransformationRepository>();

            return services;
        }
    }
}

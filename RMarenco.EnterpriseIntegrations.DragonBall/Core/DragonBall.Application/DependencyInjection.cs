using DragonBall.Application.Interfaces;
using DragonBall.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DragonBall.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<ITransformationService, TransformationService>();
            return services;
        }
    }
}

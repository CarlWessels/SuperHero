using SuperHero.API.Data.Dapper;
using SuperHero.API.Data.Interfaces;
using SuperHero.API.Data;
using SuperHero.API.Utils;
using SuperHero.API.Services.Interfaces;
using SuperHero.API.Services;

namespace SuperHero.API.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ISuperheroRepository, DapperSuperheroRepository>()
                    .AddScoped<IUserRepository, DapperUserRepository>()
                    .AddScoped<IFavoriteRepository, DapperFavoriteRepository>()
                    .AddScoped<IConnection, DapperConnection>()
                    .AddScoped<IDbConnectionFactory, DbConnectionFactory>()
                    .AddScoped<IAuthUtils, AuthUtils>()
                    .AddScoped<IUserService, UserService>()
                    ;
            return services;
        }
    }
}
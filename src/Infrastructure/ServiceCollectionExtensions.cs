using Application.Contracts.Authentication;
using Application.Contracts.Repositories;
using Application.Contracts.UnitOfWork;
using Hangfire;
using Infrastructure.Authentication;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Infrastructure;
public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(
        this IServiceCollection services, 
        string connectionString
    )
    {
        services.ConfigureDbContext(connectionString);
        services.ConfigureHealthChecks();
        services.AddAuthenticationInternal();
        services.AddUnitOfWork();
        services.AddRepositories();
        services.AddHangfireConfiguration(connectionString);
    }

    public static void AddHangfireConfiguration(this IServiceCollection services, string connectionString)
    {
        services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString)
        );

        services.AddHangfireServer();
    }

    private static void ConfigureDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }

    private static void ConfigureHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>("Database Health", HealthStatus.Unhealthy);
    }

    private static void AddAuthenticationInternal(this IServiceCollection services)
    {
        services.AddScoped<ITokenProvider, TokenProvider>()
                .AddScoped<IUserInfo, UserInfo>()
                .AddScoped<IPasswordHasher, PasswordHasher>();
    }

    private static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IPokemonRepository, PokemonRepository>()
                .AddScoped<IUserPokemonRepository, UserPokemonRepository>();
    }
}
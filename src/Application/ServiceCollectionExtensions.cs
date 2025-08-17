using Application.Contracts.Services;
using Application.Contracts.UseCases.Pokemons;
using Application.Contracts.UseCases.Users;
using Application.Services;
using Application.UseCases.Pokemons;
using Application.UseCases.Users;
using Domain.Users;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;
public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentValidation();
        services.AddServices();
        services.AddUseCases();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
    }

    private static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPokeApiService, PokeApiService>();
    }

    private static void AddUseCases(this IServiceCollection services)
    {
        services.AddUserUseCases();
        services.AddPokemonsUseCases();
    }
    private static void AddUserUseCases(this IServiceCollection services)
    {
        services.AddScoped<ILogin, Login>();
    }
    private static void AddPokemonsUseCases(this IServiceCollection services)
    {
        services.AddScoped<IUpdatePokemonsDatabase, UpdatePokemonsDatabase>();
    }
}
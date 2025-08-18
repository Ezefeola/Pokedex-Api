//using Application.Contracts.Services;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;

//namespace Infrastructure.Data.Seeders;
//public static class DataSeeder
//{
//    public async static Task InitializeDatabaseAsync(this WebApplication app)
//    {
//        using var scope = app.Services.CreateScope();
//        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//        context.Database.MigrateAsync().GetAwaiter().GetResult();

//       //await PokemonSeeder.Seed(scope.ServiceProvider.GetRequiredService<IPokeApiService>(), context);
//    }
//}
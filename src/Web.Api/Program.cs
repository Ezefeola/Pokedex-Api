using Application;
using Infrastructure;
using Infrastructure.Data.Seeders;
using Web.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region Services

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new KeyNotFoundException("Error: Connection string not found.");

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(connectionString);

builder.Services.AddApiConfig(builder.Configuration);

#endregion Services

var app = builder.Build();

#region Middlewares

if(app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}

app.AddApiWebApplicationConfig();

#endregion Middlewares

await app.RunAsync();

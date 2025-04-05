using Hahn.Application.Services;
using Hahn.Domain.Dtos;
using Hahn.Domain.Repositories;
using Hahn.Infrastructure.Persistence;
using Hahn.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hahn.WebApi.Extensions;

public static class CryptoContextExtension
{
    public static void AddCryptoServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICryptoCurrencyRepository, CryptoCurrencyRepository>();
        builder.Services.AddHttpClient<IUpsertCryptoService, UpsertCryptoService>();
    }

    public static void MapDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly("Hahn.WebApi")));
    }
    
    public static void MapCryptoEndpoints(this WebApplication app)
    {
        app.MapGet("/cryptos", async (
            [AsParameters] CryptoQueryParams query,
            IUpsertCryptoService service, CancellationToken ct) =>
        {
            var result = await service.GetAllAsync(query, ct);
            return Results.Ok(result);
        });
    }
}
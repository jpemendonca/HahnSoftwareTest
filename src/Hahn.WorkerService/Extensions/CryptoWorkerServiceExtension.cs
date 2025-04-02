using Hahn.Application.Services;
using Hahn.Domain.Repositories;
using Hahn.Infrastructure.Persistence;
using Hahn.Infrastructure.Repositories;
using Hahn.Jobs.Services;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Hahn.WorkerService.Extensions;

public static class CryptoWorkerServiceExtension
{
    public static void AddWorkerServices(this IServiceCollection services)
    {
        services.AddScoped<ICryptoCurrencyRepository, CryptoCurrencyRepository>();
        services.AddHttpClient<IUpsertCryptoService, UpsertCryptoService>();
        services.AddScoped<CryptoUpsertJob>();
    }
    
    public static void AddDbContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
    }
}
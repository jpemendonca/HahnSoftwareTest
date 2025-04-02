using Hahn.Application.Services;
using Hahn.Domain.Repositories;
using Hahn.Infrastructure.Persistence;
using Hahn.Infrastructure.Repositories;
using Hahn.Jobs.Services;
using Hangfire;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
// builder.Services.AddHostedService<Worker>();

// Add EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add DI
builder.Services.AddScoped<ICryptoCurrencyRepository, CryptoCurrencyRepository>();
builder.Services.AddHttpClient<IUpsertCryptoService, UpsertCryptoService>();
builder.Services.AddScoped<CryptoUpsertJob>();

// Add Hangfire
builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();


var host = builder.Build();

// Schedule job
using (var scope = host.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    recurringJobManager.AddOrUpdate<CryptoUpsertJob>(
        "crypto-upsert-job",
        job => job.RunAsync(),
        Cron.Hourly); // ou "0 * * * *"
}


// ✅ Run job manually at startup for debugging
// using (var scope = host.Services.CreateScope())
// {
//     var job = scope.ServiceProvider.GetRequiredService<CryptoUpsertJob>();
//     await job.RunAsync(); // 🔍 Roda aqui pra debugar
//
//     var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
//     recurringJobManager.AddOrUpdate<CryptoUpsertJob>(
//         "crypto-upsert-job",
//         j => j.RunAsync(),
//         Cron.Hourly);
// }


host.Run();
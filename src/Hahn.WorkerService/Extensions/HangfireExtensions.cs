using Hahn.Jobs.Services;
using Hangfire;

namespace Hahn.WorkerService.Extensions;

public static class HangfireExtensions
{
    public static void AddHangfireServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(x =>
            x.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));
        services.AddHangfireServer();
    }
    
    public static void ScheduleHangfireJob(this IServiceProvider services)
    {
        var recurringJobManager = services.GetRequiredService<IRecurringJobManager>();

        recurringJobManager.AddOrUpdate<CryptoUpsertJob>(
            "crypto-upsert-job",
            job => job.RunAsync(),
            Cron.Hourly);
    }
    
    public static async Task RunHangfireJobNowAsync(this IServiceProvider services)
    {
        var job = services.GetRequiredService<CryptoUpsertJob>();
        await job.RunAsync();
    }
}
using Hahn.WorkerService.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext(builder.Configuration);

// DI
builder.Services.AddWorkerServices();

// Add Hangfire
builder.Services.AddHangfireServices(builder.Configuration);

var host = builder.Build();

// Schedule job
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    scope.ServiceProvider.ScheduleHangfireJob();
}

host.Run();
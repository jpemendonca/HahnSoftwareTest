using Hahn.Application.Services;
using Microsoft.Extensions.Logging;

namespace Hahn.Jobs.Services;

public class CryptoUpsertJob
{
    private readonly IUpsertCryptoService _service;
    private readonly ILogger<CryptoUpsertJob> _logger;

    public CryptoUpsertJob(IUpsertCryptoService service, ILogger<CryptoUpsertJob> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task RunAsync()
    {
        _logger.LogInformation("Running crypto upsert job at: {Time}", DateTime.UtcNow);
        try
        {
            await _service.UpsertAsync();
            _logger.LogInformation("Upsert completed successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during crypto upsert job");
        }
    }

}
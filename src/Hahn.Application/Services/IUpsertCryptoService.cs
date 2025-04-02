using Hahn.Domain.Models;

namespace Hahn.Application.Services;

public interface IUpsertCryptoService
{
    Task UpsertAsync(CancellationToken cancellationToken = default);
    Task<List<CryptoCurrency>> GetAllAsync(CancellationToken cancellationToken = default);
}
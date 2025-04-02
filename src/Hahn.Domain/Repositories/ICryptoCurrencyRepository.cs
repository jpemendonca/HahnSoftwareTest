using Hahn.Domain.Models;

namespace Hahn.Domain.Repositories;

public interface ICryptoCurrencyRepository
{
    Task UpsertAsync(List<CryptoCurrency> cryptos, CancellationToken cancellationToken = default);
    Task<List<CryptoCurrency>> GetAllAsync(CancellationToken cancellationToken = default);
}
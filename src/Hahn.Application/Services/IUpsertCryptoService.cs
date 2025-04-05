using Hahn.Domain.Dtos;
using Hahn.Domain.Models;

namespace Hahn.Application.Services;

public interface IUpsertCryptoService
{
    Task UpsertAsync(CancellationToken cancellationToken = default);
    Task<List<CryptoCurrency>> GetAllAsync(CryptoQueryParams queryParams, CancellationToken cancellationToken = default);
}
using Hahn.Domain.Models;
using Hahn.Domain.Repositories;
using Hahn.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hahn.Infrastructure.Repositories;

public class CryptoCurrencyRepository : ICryptoCurrencyRepository
{
    private readonly AppDbContext _context;

    public CryptoCurrencyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CryptoCurrency>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Cryptos
            .OrderBy(c => c.Name)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task UpsertAsync(List<CryptoCurrency> cryptos, CancellationToken cancellationToken = default)
    {
        foreach (var crypto in cryptos)
        {
            var existing = await _context.Cryptos
                .FirstOrDefaultAsync(c => c.CoinLoreId == crypto.CoinLoreId, cancellationToken);

            if (existing is null)
            {
                await _context.Cryptos.AddAsync(crypto, cancellationToken);
            }
            else
            {
                existing.Name = crypto.Name;
                existing.Symbol = crypto.Symbol;
                existing.PriceUsd = crypto.PriceUsd;
                existing.PercentChange24h = crypto.PercentChange24h;
                existing.MarketCapUsd = crypto.MarketCapUsd;
                existing.Rank = crypto.Rank;

                _context.Cryptos.Update(existing);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
using System.Globalization;
using System.Text.Json;
using Hahn.Application.External;
using Hahn.Domain.Dtos;
using Hahn.Domain.Enums;
using Hahn.Domain.Models;
using Hahn.Domain.Repositories;

namespace Hahn.Application.Services;

public class UpsertCryptoService : IUpsertCryptoService
{
    private readonly HttpClient _httpClient;
    private readonly ICryptoCurrencyRepository _repository;

    public UpsertCryptoService(HttpClient httpClient, ICryptoCurrencyRepository repository)
    {
        _httpClient = httpClient;
        _repository = repository;
    }
    
    public async Task UpsertAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("https://api.coinlore.net/api/tickers/", cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        var result = JsonSerializer.Deserialize<CoinLoreRoot>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (result?.data == null || result.data.Count == 0)
            return;

        var cryptos = result.data.Select(c => new CryptoCurrency
        {
            CoinLoreId = Int64.Parse(c.id),
            Symbol = c.symbol,
            Name = c.name,
            Rank = c.rank,
            PriceUsd = decimal.Parse(c.price_usd, CultureInfo.InvariantCulture),
            PercentChange24h = decimal.Parse(c.percent_change_24h, CultureInfo.InvariantCulture),
            MarketCapUsd = decimal.Parse(c.market_cap_usd, CultureInfo.InvariantCulture)
        }).ToList();

        await _repository.UpsertAsync(cryptos, cancellationToken);
    }

    public async Task<List<CryptoCurrency>> GetAllAsync(CryptoQueryParams queryParams, CancellationToken cancellationToken = default)
    {
        var items = await _repository.GetAllAsync(cancellationToken);

        items = queryParams.Direction == EnumSortDirection.Desc
            ? items.OrderByDescending(c => GetSortKey(c, queryParams.SortBy)).ToList()
            : items.OrderBy(c => GetSortKey(c, queryParams.SortBy)).ToList();

        return items;
        
    }
    
    private object GetSortKey(CryptoCurrency crypto, EnumSortBy sortBy)
    {
        return sortBy switch
        {
            EnumSortBy.Name => crypto.Name,
            EnumSortBy.Symbol => crypto.Symbol,
            EnumSortBy.PriceUsd => crypto.PriceUsd,
            EnumSortBy.PercentChange24h => crypto.PercentChange24h,
            EnumSortBy.MarketCapUsd => crypto.MarketCapUsd,
            _ => crypto.Name
        };
    }
}
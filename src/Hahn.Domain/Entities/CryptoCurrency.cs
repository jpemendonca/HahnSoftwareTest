namespace Hahn.Domain.Entities;

public class CryptoCurrency
{
    public Guid Id { get; set; } = Guid.NewGuid(); // ID interno
    public long CoinLoreId { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal PriceUsd { get; set; }
    public decimal PercentChange24h { get; set; }
    public decimal MarketCapUsd { get; set; }
    public int Rank { get; set; }
}
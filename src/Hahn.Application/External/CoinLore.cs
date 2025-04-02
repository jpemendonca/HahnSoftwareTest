namespace Hahn.Application.External;

public class CoinLoreRoot
{
    public List<CoinLoreDto> data { get; set; } = [];
}

public class CoinLoreDto
{
    public string id { get; set; }
    public string symbol { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public string price_usd { get; set; } = string.Empty;
    public string percent_change_24h { get; set; } = string.Empty;
    public string market_cap_usd { get; set; } = string.Empty;
    public int rank { get; set; }
}
using Microsoft.EntityFrameworkCore;

namespace Hahn.Domain.Models;

public class CryptoCurrency : Entities.CryptoCurrency
{
    public static void Setup(ModelBuilder builder)
    {
        builder.Entity<CryptoCurrency>().HasKey(x => x.Id);
        builder.Entity<CryptoCurrency>().HasIndex(x => x.Id);
        builder.Entity<CryptoCurrency>().HasIndex(x => x.CoinLoreId);
        builder.Entity<CryptoCurrency>().Property(x => x.Id).ValueGeneratedOnAdd();
    }
}
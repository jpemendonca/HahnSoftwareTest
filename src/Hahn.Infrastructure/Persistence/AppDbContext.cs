using Hahn.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Hahn.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<CryptoCurrency> Cryptos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        CryptoCurrency.Setup(modelBuilder);

    }
}
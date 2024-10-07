using Greggs.Products.Api.Enums;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Greggs.Products.Api.DataAccess;

/// <summary>
/// DISCLAIMER: This is only here to help enable the purpose of this exercise, this doesn't reflect the way we work!
/// </summary>
public class ProductAccess : IDataAccess<Product>
{
    private static readonly IEnumerable<Product> ProductDatabase = new List<Product>()
    {
        new() { Name = "Sausage Roll", PriceInPounds = 1m},
        new() { Name = "Vegan Sausage Roll", PriceInPounds = 1.1m },
        new() { Name = "Steak Bake", PriceInPounds = 1.2m },
        new() { Name = "Yum Yum", PriceInPounds = 0.7m },
        new() { Name = "Pink Jammie", PriceInPounds = 0.5m },
        new() { Name = "Mexican Baguette", PriceInPounds = 2.1m },
        new() { Name = "Bacon Sandwich", PriceInPounds = 1.95m},
        new() { Name = "Coca Cola", PriceInPounds = 1.2m}
    };

    public async Task<IEnumerable<Product>> ListAsync(int? pageStart, int? pageSize, Currency? currency = null)
    {
        var queryable = ProductDatabase.AsQueryable();

        if (pageStart.HasValue)
            queryable = queryable.Skip(pageStart.Value);

        if (pageSize.HasValue)
            queryable = queryable.Take(pageSize.Value);

        var results = queryable.ToList();

        if (currency == null)
        {
            currency = Currency.GBP;
        }

        string fromCurrency = nameof(Currency.GBP);
        string toCurrency = currency == Currency.GBP ? nameof(Currency.GBP) : currency.ToString();

        await Parallel.ForEachAsync(results, async (product, cancellationToken) =>
        {
            product.PriceInRetail = RateExchange.ConvertCurrency(product.PriceInPounds, fromCurrency, toCurrency);
            product.currency = toCurrency;
            product.CreatedAt = GetRandomDate(new Random(), DateTime.Now.AddYears(-1), DateTime.Now);
        });

        return results.OrderByDescending(x => x.CreatedAt);
    }

    private DateTime GetRandomDate(Random random, DateTime start, DateTime end)
    {
        int range = (end - start).Days;
        return start.AddDays(random.Next(range));
    }
}
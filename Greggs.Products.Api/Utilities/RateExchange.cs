using System;
using System.Collections.Generic;

namespace Greggs.Products.Api.Utilities
{
    public static class RateExchange
    {
        private static Dictionary<string, Dictionary<string, decimal>> exchangeRates;

        static RateExchange()
        {
            exchangeRates = new Dictionary<string, Dictionary<string, decimal>>
            {
                { "GBP", new Dictionary<string, decimal> { { "GBP", 1M }, { "EUR", 1.1M } } },
                { "EUR", new Dictionary<string, decimal> { { "EUR", 1M }, { "GBP", 0.9M } } }
            };
        }

        public static decimal ConvertCurrency(decimal amount, string fromCurrency, string toCurrency)
        {
            if (exchangeRates.ContainsKey(fromCurrency) && exchangeRates[fromCurrency].ContainsKey(toCurrency))
            {
                return amount * exchangeRates[fromCurrency][toCurrency];
            }
            else
            {
                throw new ArgumentException($"Exchange rate from {fromCurrency} to {toCurrency} not available.");
            }
        }
    }
}
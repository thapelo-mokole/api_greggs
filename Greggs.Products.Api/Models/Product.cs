using System;

namespace Greggs.Products.Api.Models;

public class Product
{
    public string Name { get; set; }
    public decimal PriceInPounds { get; set; }
    public decimal? PriceInRetail { get; set; }
    public string currency { get; set; }
    public DateTime? CreatedAt { get; set; }
}
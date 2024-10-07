using Greggs.Products.Api.Enums;
using Greggs.Products.Api.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IRepository _repository;

    public ProductController(
        ILogger<ProductController> logger,
        IRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet("latest")]
    public async Task<IActionResult> Get(int pageStart = 0, int pageSize = 5)
    {
        try
        {
            var results = await _repository.GetProductsAsync(pageStart, pageSize);
            return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
        }
    }

    [HttpGet("by-currency")]
    public async Task<IActionResult> GetByCurrency(Employee employee, Currency currency = Currency.GBP, int pageStart = 0, int pageSize = 5)
    {
        try
        {
            var results = await _repository.GetProductsByCurrencyAsync(employee, currency, pageStart, pageSize);
            return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
        }
    }
}
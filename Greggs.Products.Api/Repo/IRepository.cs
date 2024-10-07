using Greggs.Products.Api.Enums;
using Greggs.Products.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Greggs.Products.Api.Repo
{
    public interface IRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(int pageStart, int pageSize);

        Task<IEnumerable<Product>> GetProductsByCurrencyAsync(Employee employee, Currency currency, int pageStart, int pageSize);
    }
}
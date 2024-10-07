using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Enums;
using Greggs.Products.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Greggs.Products.Api.Repo
{
    public class Repository : IRepository
    {
        private readonly IDataAccess<Product> _productAccess;

        public Repository(IDataAccess<Product> productAccess)
        {
            _productAccess = productAccess;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int pageStart, int pageSize)
        {
            return await _productAccess.ListAsync(pageStart, pageSize);
        }

        public async Task<IEnumerable<Product>> GetProductsByCurrencyAsync(Employee employee, Currency currency, int pageStart, int pageSize)
        {
            if (employee == Employee.None)
            {
                throw new Exception("The employee is not loggged in.");
            }

            return await _productAccess.ListAsync(pageStart, pageSize, currency);
        }
    }
}
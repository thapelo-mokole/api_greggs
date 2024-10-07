using Greggs.Products.Api.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Greggs.Products.Api.DataAccess;

public interface IDataAccess<T>
{
    Task<IEnumerable<T>> ListAsync(int? pageStart, int? pageSize, Currency? currency = null);
}
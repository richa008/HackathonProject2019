using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartShelves.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SmartShelves.Models;

    public interface ICosmosDbService
    {
        Task<IEnumerable<Product>> GetProductsAsync(string query);
        Task<Product> GetProductAsync(string id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(string id, Product product);
        Task DeleteProductAsync(string id);
    }
}

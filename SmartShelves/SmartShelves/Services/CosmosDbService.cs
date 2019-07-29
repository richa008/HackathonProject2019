using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartShelves.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using SmartShelves.Models;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Fluent;
    using Microsoft.Extensions.Configuration;

    public class CosmosDbService: ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddProductAsync(Product product)
        {
            await this._container.CreateItemAsync<Product>(product, new PartitionKey(product.Id));
        }

        public async Task DeleteProductAsync(string id)
        {
            await this._container.DeleteItemAsync<Product>(id, new PartitionKey(id));
        }

        public async Task<Product> GetProductAsync(string id)
        {
            ItemResponse<Product> response = await this._container.ReadItemAsync<Product>(id, new PartitionKey(id));
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            return response.Resource;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Product>(new QueryDefinition(queryString));
            List<Product> results = new List<Product>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateProductAsync(string id, Product item)
        {
            await this._container.UpsertItemAsync<Product>(item, new PartitionKey(id));
        }
    }
}

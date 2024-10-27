namespace ECom
{
    using ECom.Models;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductRepository(IOptions<MongoDBSettings> settings, IMongoClient client)
        {
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _productCollection = database.GetCollection<Product>("Products");
        }

        public async Task<List<Product>> GetProductsAsync() =>
            await _productCollection.Find(product => true).ToListAsync();

        public async Task<Product> GetProductByIdAsync(string id) =>
            await _productCollection.Find(product => product.Id == id).FirstOrDefaultAsync();

        public async Task CreateProductAsync(Product product) =>
            await _productCollection.InsertOneAsync(product);

        public async Task UpdateProductAsync(string id, Product productIn) =>
            await _productCollection.ReplaceOneAsync(product => product.Id == id, productIn);

        public async Task DeleteProductAsync(string id) =>
            await _productCollection.DeleteOneAsync(product => product.Id == id);
    }

}

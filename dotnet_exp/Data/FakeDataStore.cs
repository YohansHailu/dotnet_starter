public class FakeDataStore
{
    public List<Product> _products;
    public FakeDataStore()
    {
        _products = new List<Product>
        {
            new Product { Id = 1, Name = "Test Product 1" },
            new Product { Id = 2, Name = "Test Product 2" },
            new Product { Id = 3, Name = "Test Product 3" }
        };
    }

    public async Task addProduct(Product new_product)
    {
        _products.Add(new_product);

        await Task.CompletedTask;
    }

    public async Task<Product> GetProductById(int id)
    {
        // find the element and return
        // o
        var product = _products.Find(p => p.Id == id);
        if (product == null)
        {
            throw new Exception("Product not found");
        }

        return await Task.FromResult(product);
    }
    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await Task.FromResult(_products);
    }

    public async Task EventOccured(int id, string event_name)
    {
        // add even name to the product

        var product = _products.Find(p => p.Id == id);
        if (product == null)
        {
            throw new Exception("Product not found");
        }
        product.Name = $"{product.Name}  event: {event_name}";
        await Task.CompletedTask;

    }

}

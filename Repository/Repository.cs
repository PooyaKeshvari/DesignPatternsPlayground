namespace DesignPatternsPlayground;

public interface IProductRepository
{
    Product GetById(int id);
}

public sealed class InMemoryProductRepository : IProductRepository
{
    private static readonly Dictionary<int, Product> Products = new()
    {
        [1] = new Product(1, "Mechanical Keyboard", 89.99m),
        [2] = new Product(2, "Developer Mouse", 49.50m)
    };

    public Product GetById(int id)
    {
        if (!Products.TryGetValue(id, out Product? product))
        {
            throw new InvalidOperationException($"Product '{id}' was not found.");
        }

        return product;
    }
}

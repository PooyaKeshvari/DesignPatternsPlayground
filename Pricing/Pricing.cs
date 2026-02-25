using System.Collections.Concurrent;

namespace DesignPatternsPlayground;

public interface IPriceService
{
    decimal GetUnitPrice(int productId);
}

public sealed class PriceService : IPriceService
{
    private readonly IProductRepository _productRepository;

    public PriceService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public decimal GetUnitPrice(int productId)
    {
        Console.WriteLine("PriceService: fetching unit price from repository...");
        return _productRepository.GetById(productId).UnitPrice;
    }
}

public sealed class CachedPriceServiceDecorator : IPriceService
{
    private readonly IPriceService _inner;
    private readonly ConcurrentDictionary<int, decimal> _cache = new();

    public CachedPriceServiceDecorator(IPriceService inner)
    {
        _inner = inner;
    }

    public decimal GetUnitPrice(int productId)
    {
        if (_cache.TryGetValue(productId, out decimal cachedPrice))
        {
            Console.WriteLine("CachedPriceServiceDecorator: using cached price.");
            return cachedPrice;
        }

        decimal price = _inner.GetUnitPrice(productId);
        _cache[productId] = price;
        return price;
    }
}

using System.Collections.Generic;
using System.Linq;

namespace SupermarketReceipt;

public class FakeCatalog : SupermarketCatalog
{
    private readonly List<Product> _products = [];

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double GetUnitPrice(Product product)
    {
        return _products.First(p => p.Name == product.Name).UnitPrice;
    }
}
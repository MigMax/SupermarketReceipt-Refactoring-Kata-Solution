using System.Collections.Generic;

namespace SupermarketReceipt;

public class FakeCatalog : SupermarketCatalog
{
    private readonly List<Product> _products = [];

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }
}
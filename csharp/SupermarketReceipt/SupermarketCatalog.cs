namespace SupermarketReceipt;

public interface SupermarketCatalog
{
    void AddProduct(Product product);

    double GetUnitPrice(Product product);
}
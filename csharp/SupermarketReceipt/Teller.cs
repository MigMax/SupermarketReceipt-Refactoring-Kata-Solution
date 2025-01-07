using System.Collections.Generic;

namespace SupermarketReceipt;

public class Teller(SupermarketCatalog catalog)
{
    private readonly Dictionary<Product, Offer> _offers = new Dictionary<Product, Offer>();

    public void AddSpecialOffer(SpecialOfferType offerType, Product product, double argument)
    {
        _offers[product] = new Offer(offerType, argument);
    }

    public Receipt ChecksOutArticlesFrom(ShoppingCart theCart)
    {
        var receipt = new Receipt();
        
        var productQuantities = theCart.GetItems();
        
        foreach (ProductQuantity pq in productQuantities)
        {
            var p = pq.Product;
            var quantity = pq.Quantity;
            var unitPrice = catalog.GetUnitPrice(p);
            var price = quantity * unitPrice;
            receipt.AddProduct(p, quantity, unitPrice, price);
        }

        theCart.HandleOffers(receipt, _offers, catalog);

        return receipt;
    }
}
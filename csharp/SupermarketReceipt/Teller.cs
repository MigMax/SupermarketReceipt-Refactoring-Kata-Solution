using System.Collections.Generic;

namespace SupermarketReceipt;

public class Teller(SupermarketCatalog catalog)
{
    private readonly Dictionary<Product, Offer> _offers = new Dictionary<Product, Offer>();

    public void AddSpecialOffer(SpecialOfferType offerType, Product product, double argument)
    {
        _offers[product] = new Offer(offerType, argument);
    }

    public Receipt ChecksOutArticlesFrom(ShoppingCart cart)
    {
        return cart.ChecksOutArticles(catalog, _offers);
    }

    private ReceiptItem CreateReceiptItem(ProductQuantity productQuantity)
    {
        var unitPrice = catalog.GetUnitPrice(productQuantity.Product);
        
        var price = productQuantity.Quantity * unitPrice;

        return new ReceiptItem(productQuantity.Product, productQuantity.Quantity, unitPrice, price);
    }
}
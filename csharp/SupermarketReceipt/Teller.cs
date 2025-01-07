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
        
        foreach (ProductQuantity productQuantity in productQuantities)
        {
            receipt.AddItem(CreateReceiptItem(productQuantity));
        }

        theCart.HandleOffers(receipt, _offers, catalog);

        return receipt;
    }

    private ReceiptItem CreateReceiptItem(ProductQuantity productQuantity)
    {
        var unitPrice = catalog.GetUnitPrice(productQuantity.Product);
        
        var price = productQuantity.Quantity * unitPrice;

        return new ReceiptItem(productQuantity.Product, productQuantity.Quantity, price, price);
    }
}
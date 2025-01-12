using System.Collections.Generic;

namespace SupermarketReceipt;

public class Offers
{
    private readonly Dictionary<Product, Offer> _offers = new();
    
    public Offer GetOffer(Product product)
    {
        _offers.TryGetValue(product, out var offer);

        return offer;
    }
}
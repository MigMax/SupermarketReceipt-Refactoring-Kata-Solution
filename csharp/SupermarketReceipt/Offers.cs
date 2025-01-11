using System.Collections.Generic;

namespace SupermarketReceipt;

public class Offers
{
    private readonly Dictionary<Product, Offer> _offers = new();

    public void AddForProduct(Product product, Offer offer)
    {
        _offers[product] = offer;
    }

    public Offer GetOffer(Product product)
    {
        _offers.TryGetValue(product, out var offer);

        return offer;
    }
}
using System.Collections.Generic;

namespace SupermarketReceipt;

public class Offers
{
    public readonly Dictionary<Product, Offer> _offers = new();

    public void AddOffer(Product product, SpecialOfferType offerType, double argument)
    {
        _offers[product] = new Offer(offerType, argument);
    }
}
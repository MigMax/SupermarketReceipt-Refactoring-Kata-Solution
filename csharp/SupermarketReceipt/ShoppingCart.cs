using System.Collections.Generic;
using System.Globalization;

namespace SupermarketReceipt;

public class ShoppingCart
{
    private readonly List<CartItem> _items = [];
    private readonly Dictionary<Product, double> _productQuantities = new Dictionary<Product, double>();
    private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");

    public Receipt ChecksOutArticles(SupermarketCatalog catalog, Dictionary<Product, Offer> offers)
    {
        var receipt = new Receipt();

        var productQuantities = GetItems();

        foreach (CartItem productQuantity in productQuantities)
        {
            receipt.AddItem(CreateReceiptItem(productQuantity, catalog));
        }

        HandleOffers(receipt, offers, catalog);

        return receipt;
    }

    public List<CartItem> GetItems()
    {
        return [.. _items];
    }

    private ReceiptItem CreateReceiptItem(CartItem cartItem, SupermarketCatalog catalog)
    {
        var unitPrice = catalog.GetUnitPrice(cartItem.Product);

        var price = cartItem.Quantity * unitPrice;

        return new ReceiptItem(cartItem.Product, cartItem.Quantity, unitPrice, price);
    }

    public void AddItemQuantity(Product product, double quantity)
    {
        _items.Add(new CartItem(product, quantity));
        if (_productQuantities.ContainsKey(product))
        {
            var newAmount = _productQuantities[product] + quantity;
            _productQuantities[product] = newAmount;
        }
        else
        {
            _productQuantities.Add(product, quantity);
        }
    }

    public void HandleOffers(Receipt receipt, Dictionary<Product, Offer> offers, SupermarketCatalog catalog)
    {
        foreach (var product in _productQuantities.Keys)
        {
            var quantity = _productQuantities[product];

            var quantityAsInt = (int) quantity;
            
            if (offers.ContainsKey(product))
            {
                var offer = offers[product];
                
                var unitPrice = catalog.GetUnitPrice(product);

                var discount = GetDiscount(product, offer, quantityAsInt, unitPrice, quantity);

                if (discount is not null)
                {
                    receipt.AddDiscount(discount);
                }
            }
        }
    }

    private Discount GetDiscount(Product product, Offer offer, int quantityAsInt, double quantity, double unitPrice)
    {
        Discount discount = null;

        var x = 1;

        if (offer.OfferType == SpecialOfferType.ThreeForTwo)
        {
            x = 3;
        }
        else if (offer.OfferType == SpecialOfferType.TwoForAmount)
        {
            x = 2;

            if (quantityAsInt >= 2)
            {
                var total = offer.Argument * (quantityAsInt / x) + quantityAsInt % 2 * unitPrice;
                var discountN = unitPrice * quantity - total;
                discount = new Discount(product, "2 for " + PrintPrice(offer.Argument), -discountN);
            }
        }

        if (offer.OfferType == SpecialOfferType.FiveForAmount) x = 5;

        var numberOfXs = quantityAsInt / x;

        switch (offer.OfferType)
        {
            case SpecialOfferType.ThreeForTwo when quantityAsInt > 2:
            {
                var discountAmount = quantity * unitPrice - (numberOfXs * 2 * unitPrice + quantityAsInt % 3 * unitPrice);
                discount = new Discount(product, "3 for 2", -discountAmount);
                break;
            }
            case SpecialOfferType.TenPercentDiscount:
                discount = new Discount(product, offer.Argument + "% off", -quantity * unitPrice * offer.Argument / 100.0);
                break;
            case SpecialOfferType.FiveForAmount when quantityAsInt >= 5:
            {
                var discountTotal = unitPrice * quantity - (offer.Argument * numberOfXs + quantityAsInt % 5 * unitPrice);
                discount = new Discount(product, x + " for " + PrintPrice(offer.Argument), -discountTotal);
                break;
            }
        }

        return discount;
    }

    private string PrintPrice(double price)
    {
        return price.ToString("N2", Culture);
    }
}
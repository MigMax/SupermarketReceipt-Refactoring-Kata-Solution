using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SupermarketReceipt;

public class ShoppingCart(Offers offers)
{
    private readonly List<CartItem> _cartItems = [];
    
    private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");

    public Receipt ChecksOutArticles()
    {
        var receipt = new Receipt();

        var productQuantities = GetItems();

        foreach (CartItem cartItem in productQuantities)
        {
            var receiptItem = CreateReceiptItem(cartItem);
            receipt.AddItem(receiptItem);
        }

        ApplyDiscounts(receipt);

        return receipt;
    }

    private List<CartItem> GetItems()
    {
        return [.. _cartItems];
    }

    private ReceiptItem CreateReceiptItem(CartItem cartItem)
    {
        var unitPrice = cartItem.Product.UnitPrice;

        var price = cartItem.Quantity * unitPrice;

        return new ReceiptItem(cartItem.Product, cartItem.Quantity, unitPrice, price);
    }

    public void AddCartItem(CartItem cartItem)
    {
        var existingCartItem = _cartItems.Find(item => item.Product.Name == cartItem.Product.Name);
        
        if (existingCartItem is not null)
        {
            existingCartItem.AddQuantity(cartItem.Quantity);
        }
        else
        {
            _cartItems.Add(cartItem);
        }
    }

    private void ApplyDiscounts(Receipt receipt)
    {
        foreach (var cartItem in _cartItems)
        {
            var offer = offers.GetOffer(cartItem.Product);
            
            if (offer is not null)
            {
                CalculateAndApplyDiscount(receipt, cartItem, offer);
            }
        }
    }

    private void CalculateAndApplyDiscount(Receipt receipt, CartItem cartItem, Offer offer)
    {
        var discount = ComputeDiscount(cartItem, offer);

        if (discount is not null)
        {
            receipt.AddDiscount(discount);
        }
    }

    private Discount ComputeDiscount(CartItem cartItem, Offer offer)
    {
        var quantity = cartItem.Quantity;

        var quantityAsInt = (int)quantity;
        
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
                var total = offer.Argument * (quantityAsInt / x) + quantityAsInt % 2 * cartItem.Product.UnitPrice;
                var discountN = cartItem.Product.UnitPrice * quantity - total;
                discount = new Discount(cartItem.Product, "2 for " + PrintPrice(offer.Argument), -discountN);
            }
        }

        if (offer.OfferType == SpecialOfferType.FiveForAmount) x = 5;

        var numberOfXs = quantityAsInt / x;

        switch (offer.OfferType)
        {
            case SpecialOfferType.ThreeForTwo when quantityAsInt > 2:
            {
                var discountAmount = quantity * cartItem.Product.UnitPrice - (numberOfXs * 2 * cartItem.Product.UnitPrice + quantityAsInt % 3 * cartItem.Product.UnitPrice);
                discount = new Discount(cartItem.Product, "3 for 2", -discountAmount);
                break;
            }
            case SpecialOfferType.TenPercentDiscount:
                discount = new Discount(cartItem.Product, offer.Argument + "% off", -quantity * cartItem.Product.UnitPrice * offer.Argument / 100.0);
                break;
            case SpecialOfferType.FiveForAmount when quantityAsInt >= 5:
            {
                var discountTotal = cartItem.Product.UnitPrice * quantity - (offer.Argument * numberOfXs + quantityAsInt % 5 * cartItem.Product.UnitPrice);
                discount = new Discount(cartItem.Product, x + " for " + PrintPrice(offer.Argument), -discountTotal);
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
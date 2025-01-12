using System.Collections.Generic;
using System.Globalization;

namespace SupermarketReceipt;

public class Product(string name, ProductUnit unit, double price)
{
    public string Name { get; } = name;
    public ProductUnit Unit { get; } = unit;
    public double UnitPrice { get; } = price;
    
    private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");

    public Discount GetDiscount(Offer offer, double quantity)
    {
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
                var total = offer.Argument * (quantityAsInt / x) + quantityAsInt % 2 * UnitPrice;
                var discountN = UnitPrice * quantity - total;
                discount = new Discount(this, "2 for " + PrintPrice(offer.Argument), -discountN);
            }
        }

        if (offer.OfferType == SpecialOfferType.FiveForAmount) x = 5;

        var numberOfXs = quantityAsInt / x;

        switch (offer.OfferType)
        {
            case SpecialOfferType.ThreeForTwo when quantityAsInt > 2:
            {
                var discountAmount = quantity * UnitPrice - (numberOfXs * 2 * UnitPrice + quantityAsInt % 3 * UnitPrice);
                discount = new Discount(this, "3 for 2", -discountAmount);
                break;
            }
            case SpecialOfferType.TenPercentDiscount:
                discount = new Discount(this, offer.Argument + "% off", -quantity * UnitPrice * offer.Argument / 100.0);
                break;
            case SpecialOfferType.FiveForAmount when quantityAsInt >= 5:
            {
                var discountTotal = UnitPrice * quantity - (offer.Argument * numberOfXs + quantityAsInt % 5 * UnitPrice);
                discount = new Discount(this, x + " for " + PrintPrice(offer.Argument), -discountTotal);
                break;
            }
        }

        return discount;
    }
    
    private string PrintPrice(double price)
    {
        return price.ToString("N2", Culture);
    }
    
    public override bool Equals(object obj)
    {
        var product = obj as Product;
        
        return product != null &&
               Name == product.Name &&
               Unit == product.Unit;
    }

    public override int GetHashCode()
    {
        var hashCode = -1996304355;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
        hashCode = hashCode * -1521134295 + Unit.GetHashCode();
        return hashCode;
    }
}
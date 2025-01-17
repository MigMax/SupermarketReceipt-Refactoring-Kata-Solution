using System.Collections.Generic;
using System.Globalization;

namespace SupermarketReceipt;

public class Product(string name, ProductUnit unit, double price, Offer? offer = null)
{
    public string Name { get; } = name;
    public ProductUnit Unit { get; } = unit;
    public double UnitPrice { get; } = price;
    
    public Offer? Offer { get; } = offer;
    
    private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");

    public Discount? GetDiscount(double quantity)
    {
        if (offer is null)
        {
            return null;
        }
        
        var quantityAsInt = (int)quantity;

        if (offer.OfferType == SpecialOfferType.TwoForAmount && quantityAsInt >= 2)
        {
            var total = offer.Argument * (quantityAsInt / 2) + quantityAsInt % 2 * UnitPrice;
            var discountN = UnitPrice * quantity - total;
            return new Discount(Name, "2 for " + PrintPrice(offer.Argument), -discountN);
        }
        
        if (offer.OfferType == SpecialOfferType.ThreeForTwo && quantityAsInt > 2)
        {
            var discountAmount = -(quantity * UnitPrice - (DiscountPercentage(quantityAsInt, 3) * 2 * UnitPrice + quantityAsInt % 3 * UnitPrice));
            var description = "3 for 2";
            return new Discount(Name, description, discountAmount);
        }

        if (offer.OfferType == SpecialOfferType.TenPercentDiscount)
        {
            var discountAmount = -quantity * UnitPrice * offer.Argument / 100.0;
            var description = offer.Argument + "% off";
            return new Discount(Name, description, discountAmount);
        }

        if (offer.OfferType == SpecialOfferType.FiveForAmount && quantityAsInt >= 5)
        {
            var discountAmount = -(UnitPrice * quantity - (offer.Argument * DiscountPercentage(quantityAsInt, 5) + quantityAsInt % 5 * UnitPrice));
            var description = 5 + " for " + PrintPrice(offer.Argument);
            return new Discount(Name, description, discountAmount);
        }

        return null;
    }

    private static int DiscountPercentage(int initialQuantityBuy, int totalQuantityWithOffer)
    {
        return initialQuantityBuy / totalQuantityWithOffer;
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
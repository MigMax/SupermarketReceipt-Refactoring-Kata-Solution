using System.Globalization;

namespace SupermarketReceipt;

public class CartItem(Product product, double quantity)
{
    public Product Product { get; } = product;
    public double Quantity { get; private set; } = quantity;
    
    private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");
    

    public void AddQuantity(double quantity)
    {
        Quantity += quantity;
    }
    
    public Discount ComputeDiscount(Offer offer)
    {
        var quantity = Quantity;

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
                var total = offer.Argument * (quantityAsInt / x) + quantityAsInt % 2 * Product.UnitPrice;
                var discountN = Product.UnitPrice * quantity - total;
                discount = new Discount(Product, "2 for " + PrintPrice(offer.Argument), -discountN);
            }
        }

        if (offer.OfferType == SpecialOfferType.FiveForAmount) x = 5;

        var numberOfXs = quantityAsInt / x;

        switch (offer.OfferType)
        {
            case SpecialOfferType.ThreeForTwo when quantityAsInt > 2:
            {
                var discountAmount = quantity * Product.UnitPrice - (numberOfXs * 2 * Product.UnitPrice + quantityAsInt % 3 * Product.UnitPrice);
                discount = new Discount(Product, "3 for 2", -discountAmount);
                break;
            }
            case SpecialOfferType.TenPercentDiscount:
                discount = new Discount(Product, offer.Argument + "% off", -quantity * Product.UnitPrice * offer.Argument / 100.0);
                break;
            case SpecialOfferType.FiveForAmount when quantityAsInt >= 5:
            {
                var discountTotal = Product.UnitPrice * quantity - (offer.Argument * numberOfXs + quantityAsInt % 5 * Product.UnitPrice);
                discount = new Discount(Product, x + " for " + PrintPrice(offer.Argument), -discountTotal);
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
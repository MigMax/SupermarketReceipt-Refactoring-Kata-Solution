using System.Globalization;

namespace SupermarketReceipt;

public class CartItem(Product product, double quantity)
{
    public Product Product { get; } = product;
    public double Quantity { get; private set; } = quantity;
    
    public void AddQuantity(double quantity)
    {
        Quantity += quantity;
    }
    
    public Discount ComputeDiscount(Offer offer)
    {
        return Product.GetDiscount(offer, Quantity);
    }
}
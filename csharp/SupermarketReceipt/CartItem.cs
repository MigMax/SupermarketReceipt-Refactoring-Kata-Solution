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
    
    public ReceiptItem CreateReceiptItem()
    {
        var unitPrice = Product.UnitPrice;

        var price = Quantity * unitPrice;

        return new ReceiptItem(Product, Quantity, unitPrice, price);
    }
    
    public Discount? ComputeDiscount()
    {
        return Product.GetDiscount(Quantity);
    }
}
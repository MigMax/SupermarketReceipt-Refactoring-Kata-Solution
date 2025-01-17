namespace SupermarketReceipt;

public class ReceiptItem(Product p, double quantity, double unitPrice, double totalPrice)
{
    public Product Product { get; } = p;
    public double UnitPrice { get; } = unitPrice;
    public double TotalPrice { get; } = totalPrice;
    public double Quantity { get; } = quantity;
}
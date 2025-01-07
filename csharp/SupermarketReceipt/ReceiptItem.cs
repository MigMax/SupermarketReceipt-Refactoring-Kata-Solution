namespace SupermarketReceipt;

public class ReceiptItem(Product p, double quantity, double price, double totalPrice)
{
    public Product Product { get; } = p;
    public double Price { get; } = price;
    public double TotalPrice { get; } = totalPrice;
    public double Quantity { get; } = quantity;
}
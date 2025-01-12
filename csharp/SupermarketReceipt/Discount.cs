namespace SupermarketReceipt;

public class Discount(string productName, string description, double discountAmount)
{
    public string Description { get; } = description;
    public double DiscountAmount { get; } = discountAmount;
    public string ProductName { get; } = productName;
}
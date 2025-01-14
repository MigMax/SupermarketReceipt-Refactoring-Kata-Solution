namespace SupermarketReceipt;

public sealed record CartItem(Product product, double quantity)
{
    public Product Product { get; } = product;
    private double Quantity { get; set; } = quantity;
    
    public CartItem AddQuantity(double quantity)
    {
        return this with
        {
            Quantity = Quantity + quantity
        };
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
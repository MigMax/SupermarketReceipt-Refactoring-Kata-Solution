using System.Collections.Generic;
using System.Linq;

namespace SupermarketReceipt;

public class Receipt
{
    private readonly List<Discount> _discounts = [];
    private readonly List<ReceiptItem> _items = [];

    public double GetTotalPrice()
    {
        return _items.Sum(item => item.TotalPrice) + _discounts.Sum(discount => discount.DiscountAmount);
    }

    public void AddProduct(Product p, double quantity, double price, double totalPrice)
    {
        _items.Add(new ReceiptItem(p, quantity, price, totalPrice));
    }

    public List<ReceiptItem> GetItems()
    {
        return [.._items];
    }

    public void AddDiscount(Discount discount)
    {
        _discounts.Add(discount);
    }

    public List<Discount> GetDiscounts()
    {
        return _discounts;
    }
}

public class ReceiptItem(Product p, double quantity, double price, double totalPrice)
{
    public Product Product { get; } = p;
    public double Price { get; } = price;
    public double TotalPrice { get; } = totalPrice;
    public double Quantity { get; } = quantity;
}
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

    public void AddItem(ReceiptItem item)
    {
        _items.Add(item);
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
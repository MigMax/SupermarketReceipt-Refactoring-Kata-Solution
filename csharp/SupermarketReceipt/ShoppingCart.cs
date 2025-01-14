using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SupermarketReceipt;

public class ShoppingCart()
{
    private readonly List<CartItem> _cartItems = [];
    
    public Receipt ChecksOutArticles()
    {
        var receipt = new Receipt();
        
        foreach (CartItem cartItem in _cartItems)
        {
            var receiptItem = cartItem.CreateReceiptItem();
            receipt.AddItem(receiptItem);
        }

        ApplyDiscounts(receipt);

        return receipt;
    }

    public void AddOrUpdateCartItem(Product product, double quantity)
    {
        var existingCartItem = _cartItems.Find(item => item.Product.Name == product.Name);

        if (existingCartItem is null)
        {
            _cartItems.Add(new CartItem(product, quantity));
        }
        else
        {
            _cartItems.Remove(existingCartItem);
            _cartItems.Add(existingCartItem.AddQuantity(quantity));
        }
    }

    private void ApplyDiscounts(Receipt receipt)
    {
        foreach (var cartItem in _cartItems)
        {
            var discount = cartItem.ComputeDiscount();

            if (discount is not null)
            {
                receipt.AddDiscount(discount);
            }
        }
    }
}
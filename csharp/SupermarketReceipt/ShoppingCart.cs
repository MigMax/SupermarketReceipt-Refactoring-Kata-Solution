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

    public void AddCartItem(CartItem cartItem)
    {
        var existingCartItem = _cartItems.Find(item => item.Product.Name == cartItem.Product.Name);
        
        if (existingCartItem is not null)
        {
            existingCartItem.AddQuantity(cartItem.Quantity);
        }
        else
        {
            _cartItems.Add(cartItem);
        }
    }

    private void ApplyDiscounts(Receipt receipt)
    {
        foreach (var cartItem in _cartItems)
        {
            if (cartItem.Product.Offer is null)
            {
                continue;
            }
            
            var discount = cartItem.ComputeDiscount();

            if (discount is not null)
            {
                receipt.AddDiscount(discount);
            }
        }
    }
}
using System.Collections.Generic;
using Xunit;

namespace SupermarketReceipt.Test;

public class SupermarketXUnitTest
{
    [Fact]
    public void TenPercentDiscount()
    {
        SupermarketCatalog catalog = new FakeCatalog();
        
        var toothbrush = new Product("toothbrush", ProductUnit.Each, 0.99);
        
        catalog.AddProduct(toothbrush);
        
        var apples = new Product("apples", ProductUnit.Kilo, 1.99);
        
        catalog.AddProduct(apples);

        var offers = new Offers();
        
        offers.AddForProduct(toothbrush, new Offer(SpecialOfferType.TenPercentDiscount, 10.0));
        
        var cart = new ShoppingCart(offers);
        
        cart.AddCartItem(new CartItem(apples, 2.5));


        // ACT
        var receipt = cart.ChecksOutArticles();

        // ASSERT
        Assert.Equal(4.975, receipt.GetTotalPrice());
        Assert.Equal([], receipt.GetDiscounts());
        Assert.Single(receipt.GetItems());
        var receiptItem = receipt.GetItems()[0];
        Assert.Equal(apples, receiptItem.Product);
        Assert.Equal(1.99, receiptItem.Price);
        Assert.Equal(2.5 * 1.99, receiptItem.TotalPrice);
        Assert.Equal(2.5, receiptItem.Quantity);
    }
}
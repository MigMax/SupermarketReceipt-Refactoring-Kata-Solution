using Xunit;

namespace SupermarketReceipt.Test;

public class SupermarketXUnitTest
{
    [Fact]
    public void TenPercentDiscount()
    {
        var toothbrush 
            = new Product("toothbrush", ProductUnit.Each, 0.99, new Offer(SpecialOfferType.TenPercentDiscount, 10.0));
        
        var apples 
            = new Product("apples", ProductUnit.Kilo, 1.99);
        
        var cart = new ShoppingCart();
        
        cart.AddOrUpdateCartItem(apples, 2.5);
        
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
    
    [Fact]
    public void AddAnExistingItemToTheCart_ShouldIncreaseTheQuantity()
    {
        var apples = new Product("apples", ProductUnit.Kilo, 1.99);
        
        var cart = new ShoppingCart();
        
        cart.AddOrUpdateCartItem(apples, 2.5);
        cart.AddOrUpdateCartItem(apples, 2.5);
        
        // ACT
        var receipt = cart.ChecksOutArticles();
        
        var receiptItem = receipt.GetItems()[0];
        
        Assert.Equal(5, receiptItem.Quantity);
    }
}
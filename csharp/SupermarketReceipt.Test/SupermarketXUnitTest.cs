using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
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
        
        var receipt = cart.ChecksOutArticles();

        receipt.GetTotalPrice()
            .Should()
            .Be(4.975);
        
        receipt.GetDiscounts()
            .Should()
            .BeEquivalentTo(new List<Discount>());
        
        receipt.GetItems().Count
            .Should()
            .Be(1);
        
        var receiptItem = receipt.GetItems().Single();

        receiptItem.Product
            .Should()
            .BeEquivalentTo(apples);
        
        receiptItem.Price
            .Should()
            .Be(1.99);
        
        receiptItem.TotalPrice
            .Should()
            .Be(2.5 * 1.99);
        
        receiptItem.Quantity
            .Should()
            .Be(2.5); 
        
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
        
        receipt.GetItems().Count.Should().Be(1);
        
        var receiptItem = receipt.GetItems()[0];
        
        Assert.Equal(5, receiptItem.Quantity);
    }
}
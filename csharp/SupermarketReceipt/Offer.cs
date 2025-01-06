namespace SupermarketReceipt;

public enum SpecialOfferType
{
    ThreeForTwo,
    TenPercentDiscount,
    TwoForAmount,
    FiveForAmount
}

public class Offer(SpecialOfferType offerType, Product product, double argument)
{
    private Product _product = product;

    public SpecialOfferType OfferType { get; } = offerType;
    public double Argument { get; } = argument;
}
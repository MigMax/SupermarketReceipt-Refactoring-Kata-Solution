namespace SupermarketReceipt;

public enum SpecialOfferType
{
    ThreeForTwo,
    TenPercentDiscount,
    TwoForAmount,
    FiveForAmount
}

public class Offer(SpecialOfferType offerType, double argument)
{
    public SpecialOfferType OfferType { get; } = offerType;
    public double Argument { get; } = argument;
}
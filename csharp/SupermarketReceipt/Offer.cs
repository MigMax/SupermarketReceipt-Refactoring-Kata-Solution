namespace SupermarketReceipt;

public class Offer(SpecialOfferType offerType, double argument)
{
    public SpecialOfferType OfferType { get; } = offerType;
    public double Argument { get; } = argument;
}
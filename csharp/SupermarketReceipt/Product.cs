using System.Collections.Generic;

namespace SupermarketReceipt;

public class Product(string name, ProductUnit unit)
{
    public string Name { get; } = name;
    public ProductUnit Unit { get; } = unit;

    public override bool Equals(object obj)
    {
        var product = obj as Product;
        
        return product != null &&
               Name == product.Name &&
               Unit == product.Unit;
    }

    public override int GetHashCode()
    {
        var hashCode = -1996304355;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
        hashCode = hashCode * -1521134295 + Unit.GetHashCode();
        return hashCode;
    }
}
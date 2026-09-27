namespace SrpLab;

public sealed class GiftMessageFormatter
{
    public string Format(CheckoutBasket basket, string fromName, decimal total)
    {
        var items = string.Join(", ", basket.Lines.Select(l => l.Sku));
        return $"Dear friend,\nA gift from {fromName} awaits ({items}).\nTotal surprise value: {total:C}\n";
    }
}

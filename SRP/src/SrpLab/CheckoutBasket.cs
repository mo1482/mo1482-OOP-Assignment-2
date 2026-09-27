namespace SrpLab;

public sealed class CheckoutBasket
{
    private readonly List<(string Sku, decimal Price, int Qty)> _lines = new();

    public string? CouponRaw { get; private set; }
    public bool GiftWrapEnabled { get; private set; }

    public void AddLine(string sku, decimal price, int qty)
    {
        if (qty <= 0) throw new ArgumentOutOfRangeException(nameof(qty));
        _lines.Add((sku, price, qty));
    }

    public void ApplyCouponText(string? couponText) => CouponRaw = couponText;
    public void EnableGiftWrap() => GiftWrapEnabled = true;

    public IReadOnlyList<(string Sku, decimal Price, int Qty)> Lines => _lines;
}

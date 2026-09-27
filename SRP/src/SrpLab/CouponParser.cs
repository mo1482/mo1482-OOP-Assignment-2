namespace SrpLab;

public sealed class CouponParser
{
    public decimal GetDiscountPercent(string? couponText)
    {
        if (string.IsNullOrWhiteSpace(couponText)) return 0m;

        var text = couponText.Trim().ToUpperInvariant();

        if (text.StartsWith("SAVE") &&
            int.TryParse(text[4..], out var pct) &&
            pct is > 0 and <= 50)
            return pct;

        return 0m;
    }

    public bool IsFreeShipping(string? couponText) =>
        !string.IsNullOrWhiteSpace(couponText) &&
        couponText.Trim().ToUpperInvariant().Contains("FREESHIP");

    public bool IsWelcome10(string? couponText) =>
        string.Equals(couponText?.Trim(), "WELCOME10", StringComparison.OrdinalIgnoreCase);
}

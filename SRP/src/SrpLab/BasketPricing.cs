namespace SrpLab;

public sealed class BasketPricing
{
    public decimal SubTotal(CheckoutBasket basket)
    {
        decimal total = 0m;
        foreach (var line in basket.Lines)
            total += line.Price * line.Qty;
        return total;
    }

    public decimal DiscountAmount(CheckoutBasket basket, CouponParser coupons)
    {
        var subtotal = SubTotal(basket);
        var pct = coupons.GetDiscountPercent(basket.CouponRaw);

        if (pct > 0) return Math.Round(subtotal * pct / 100m, 2);
        if (coupons.IsWelcome10(basket.CouponRaw)) return Math.Min(10m, subtotal);

        return 0m;
    }

    public decimal GrandTotal(CheckoutBasket basket, CouponParser coupons)
    {
        var total = SubTotal(basket) - DiscountAmount(basket, coupons);

        if (basket.GiftWrapEnabled)
            total += 4.99m;

        return Math.Max(0m, total);
    }
}

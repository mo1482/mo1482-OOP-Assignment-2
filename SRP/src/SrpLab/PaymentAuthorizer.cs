namespace SrpLab;

public sealed class PaymentAuthorizer
{
    public string Authorize(CheckoutBasket basket, decimal total, string cardLast4)
    {
        var payload = $"{total:0.00}|{cardLast4}|{basket.Lines.Count}";
        var hash = payload.GetHashCode();
        return $"AUTH-{Math.Abs(hash):X8}";
    }
}

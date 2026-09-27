namespace SrpLab;

public sealed class LedgerFormatter
{
    public string Format(SubscriptionBilling subscription, string invoice, decimal proratedAmount) =>
        $"{subscription.CustomerId},{invoice},{proratedAmount:0.00},AR-SUB";
}

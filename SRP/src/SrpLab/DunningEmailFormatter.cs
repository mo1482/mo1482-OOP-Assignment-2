namespace SrpLab;

public sealed class DunningEmailFormatter
{
    public string Format(SubscriptionBilling subscription, string customerName, DateOnly asOf, decimal amount, string invoice)
    {
        var severity = subscription.FailedPayments switch
        {
            <= 1 => "friendly reminder",
            2 => "second notice",
            _ => "final notice before suspension"
        };

        return $"Subject: {severity} {invoice}
Hi {customerName}
Balance {amount:C} as of {asOf:o} ({subscription.FailedPayments} failures).
";
    }
}

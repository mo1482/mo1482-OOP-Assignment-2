namespace SrpLab;

public sealed class ThermalTicketFormatter
{
    private readonly AllergenDetector _allergens;
    private readonly KitchenEtaCalculator _eta;

    public ThermalTicketFormatter(AllergenDetector allergens, KitchenEtaCalculator eta)
    {
        _allergens = allergens;
        _eta = eta;
    }

    public string Render(KitchenTicket ticket, int orderNumber)
    {
        var width = 32;
        var line = new string('=', width);
        var body = string.Join('
', ticket.Items.Select(i => $"* {i.Item.ToUpperInvariant()} ({i.PrepMinutes}m)"));
        var allergens = _allergens.Detect(ticket);
        var allergyLine = allergens.Count == 0
            ? "ALLERGENS: none"
            : "ALLERGENS: " + string.Join(",", allergens);

        return $"{line}
ORDER #{orderNumber}
ETA {_eta.Calculate(ticket, 2)} MIN
{body}
{allergyLine}
{line}
";
    }
}

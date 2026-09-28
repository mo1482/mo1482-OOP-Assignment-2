namespace SrpLab;

public sealed class KitchenEtaCalculator
{
    private readonly AllergenDetector _allergens;

    public KitchenEtaCalculator(AllergenDetector allergens) => _allergens = allergens;

    public int Calculate(KitchenTicket ticket, int openStations)
    {
        if (openStations <= 0) openStations = 1;

        var sequential = ticket.Items.Sum(i => i.PrepMinutes);
        var parallel = (int)Math.Ceiling(sequential / (double)openStations);

        if (_allergens.Detect(ticket).Count > 0)
            parallel += 3;

        var longest = ticket.Items.Count == 0 ? 0 : ticket.Items.Max(i => i.PrepMinutes);
        return Math.Max(parallel, longest);
    }
}

namespace SrpLab;

public sealed class ExpoLanePolicy
{
    private readonly AllergenDetector _allergens;
    private readonly KitchenEtaCalculator _eta;

    public ExpoLanePolicy(AllergenDetector allergens, KitchenEtaCalculator eta)
    {
        _allergens = allergens;
        _eta = eta;
    }

    public string GetHint(KitchenTicket ticket)
    {
        return _allergens.Detect(ticket).Count > 0
            ? "LANE-ALLERGY"
            : _eta.Calculate(ticket, 2) > 20
                ? "LANE-SLOW"
                : "LANE-FAST";
    }
}

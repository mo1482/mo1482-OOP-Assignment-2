namespace SrpLab;

public sealed class LoanDocumentPolicy
{
    public IReadOnlyList<string> Required(LoanApplication loan, bool eligible)
    {
        var docs = new List<string>
        {
            "National ID",
            "Proof of income (3 months)"
        };

        if (loan.RequestedAmount > 40_000m) docs.Add("Bank statements (6 months)");
        if (loan.HasCollateral) docs.Add("Collateral ownership deed");
        if (loan.EmploymentMonths < 12) docs.Add("Employer letter");
        if (!eligible) docs.Add("Manual underwriter referral form");

        return docs;
    }
}

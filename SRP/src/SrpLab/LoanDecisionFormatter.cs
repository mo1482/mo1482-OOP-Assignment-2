namespace SrpLab;

public sealed class LoanDecisionFormatter
{
    public string DecisionLetter(string applicantName, LoanApplication loan, decimal risk, bool eligible, IReadOnlyList<string> docs)
    {
        if (eligible)
        {
            return $"Dear {applicantName},\nYour request for {loan.RequestedAmount:C} is pre-approved (risk {risk:0}).\n" +
                   $"Please upload: {string.Join("; ", docs)}.\n";
        }

        return $"Dear {applicantName},\nWe are unable to approve {loan.RequestedAmount:C} at this time.\n" +
               $"Reference risk={risk:0}. You may reapply after improving documentation.\n";
    }

    public string UnderwriterCsvRow(string applicationId, LoanApplication loan, decimal risk, bool eligible) =>
        $"{applicationId},{loan.CreditScore},{loan.EmploymentMonths},{(loan.HasCollateral ? 1 : 0)},{risk:0.00},{(eligible ? "Y" : "N")}";
}

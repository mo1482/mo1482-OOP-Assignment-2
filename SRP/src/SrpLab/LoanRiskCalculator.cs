namespace SrpLab;

public sealed class LoanRiskCalculator
{
    public decimal Calculate(LoanApplication loan)
    {
        decimal score = 100m;
        score -= Math.Max(0, 700 - loan.CreditScore) * 0.15m;
        if (loan.EmploymentMonths < 6) score -= 20m;
        if (loan.RequestedAmount > 50_000m && !loan.HasCollateral) score -= 25m;
        if (loan.RequestedAmount > 150_000m) score -= 10m;
        return Math.Clamp(score, 0m, 100m);
    }

    public bool IsEligible(LoanApplication loan)
    {
        return Calculate(loan) >= 55m && loan.CreditScore >= 580;
    }
}

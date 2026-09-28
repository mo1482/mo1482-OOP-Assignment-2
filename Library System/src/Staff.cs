namespace LibrarySystem;

public class Staff : Person
{
    public DateTime HireDate { get; }

    public decimal MonthlySalary { get; private set; }

    protected decimal ResponsibilityAllowance { get; }

    protected Staff(
        string personId,
        string fullName,
        string phone,
        DateTime hireDate,
        decimal monthlySalary,
        decimal responsibilityAllowance)
        : base(personId, fullName, phone)
    {
        if (monthlySalary <= 0)
            throw new ArgumentException("Salary must be positive.");

        HireDate = hireDate;
        MonthlySalary = monthlySalary;
        ResponsibilityAllowance = responsibilityAllowance;
    }

    public void GiveRaise(decimal percentage)
    {
        if (percentage <= 0)
            throw new ArgumentException(
                "Raise percentage must be greater than zero.");

        MonthlySalary += MonthlySalary * percentage / 100m;
    }

    public decimal GetMonthlyPay()
    {
        return MonthlySalary + ResponsibilityAllowance;
    }
}

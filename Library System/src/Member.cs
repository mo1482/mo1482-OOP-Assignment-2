namespace LibrarySystem;

public class Member : Person
{
    private readonly int maxLoans;
    private readonly decimal discountPercentage;
    private readonly List<Loan> loans;

    public IReadOnlyList<Loan> Loans
    {
        get
        {
            return loans.AsReadOnly();
        }
    }

    protected Member(
        string personId,
        string fullName,
        string phone,
        int maxLoans,
        decimal discountPercentage)
        : base(personId, fullName, phone)
    {
        if (maxLoans <= 0)
            throw new ArgumentException("Maximum loans must be positive.");

        if (discountPercentage < 0)
            throw new ArgumentException("Discount cannot be negative.");

        this.maxLoans = maxLoans;
        this.discountPercentage = discountPercentage;
        loans = new List<Loan>();
    }

    public void Borrow(Loan loan)
    {
        if (loan == null)
            throw new ArgumentException("Loan cannot be null.");

        int activeLoans = 0;

        for (int i = 0; i < loans.Count; i++)
        {
            if (loans[i].Status == LoanStatus.Borrowed)
                activeLoans++;
        }

        if (activeLoans >= maxLoans)
            throw new InvalidOperationException(
                "Member has reached the maximum number of active loans.");

        if (loan.Member != this)
            throw new InvalidOperationException(
                "A loan must belong to the member borrowing it.");

        loan.Item.CanBeBorrowed();

        loans.Add(loan);
        loan.Item.SetOnLoan();
    }

    public decimal GetDiscountAmount(decimal fee)
    {
        return fee * discountPercentage / 100m;
    }
}

namespace LibrarySystem;

public class LibraryItem
{
    public string CatalogNumber { get; }
    public string Title { get; }

    public decimal BaseLateFee { get; private set; }

    public bool IsWithdrawn { get; private set; }

    public bool IsOnLoan { get; private set; }

    private readonly int loanPeriod;
    private readonly decimal feeMultiplier;

    public int LoanPeriod
    {
        get
        {
            return loanPeriod;
        }
    }

    protected LibraryItem(
        string catalogNumber,
        string title,
        decimal baseLateFee,
        int loanPeriod,
        decimal feeMultiplier)
    {
        if (string.IsNullOrEmpty(catalogNumber))
            throw new ArgumentException(
                "Catalog number cannot be empty.");

        if (string.IsNullOrEmpty(title))
            throw new ArgumentException(
                "Title cannot be empty.");

        if (baseLateFee <= 0)
            throw new ArgumentException(
                "Late fee must be greater than zero.");

        if (loanPeriod <= 0)
            throw new ArgumentException(
                "Loan period must be positive.");

        CatalogNumber = catalogNumber;
        Title = title;
        BaseLateFee = baseLateFee;
        this.loanPeriod = loanPeriod;
        this.feeMultiplier = feeMultiplier;

        IsWithdrawn = false;
        IsOnLoan = false;
    }

    public decimal DailyLateFee
    {
        get
        {
            return BaseLateFee * feeMultiplier;
        }
    }

    public void SetLateFee(decimal newFee)
    {
        if (newFee <= 0)
            throw new ArgumentException(
                "Late fee must be greater than zero.");

        BaseLateFee = newFee;
    }

    public void Withdraw()
    {
        if (IsOnLoan)
            throw new InvalidOperationException(
                "An item on loan cannot be withdrawn.");

        IsWithdrawn = true;
    }

    public void Restore()
    {
        IsWithdrawn = false;
    }

    public void CanBeBorrowed()
    {
        if (IsWithdrawn)
            throw new InvalidOperationException(
                "A withdrawn item cannot be borrowed.");

        if (IsOnLoan)
            throw new InvalidOperationException(
                "An item already on loan cannot be borrowed.");
    }

    internal void SetOnLoan()
    {
        IsOnLoan = true;
    }

    internal void SetReturned()
    {
        IsOnLoan = false;
    }
}

namespace LibrarySystem;

public class HeadLibrarian : Staff
{
    public HeadLibrarian(
        string personId,
        string fullName,
        string phone,
        DateTime hireDate,
        decimal monthlySalary)
        : base(personId, fullName, phone, hireDate, monthlySalary, 400m)
    {
    }

    public void ChangeLateFee(LibraryItem item, decimal newFee)
    {
        item.SetLateFee(newFee);
    }

    public void WithdrawItem(LibraryItem item)
    {
        item.Withdraw();
    }

    public void RestoreItem(LibraryItem item)
    {
        item.Restore();
    }
}

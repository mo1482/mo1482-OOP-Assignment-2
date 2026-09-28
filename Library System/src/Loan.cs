namespace LibrarySystem;

public class Loan
{
    public int LoanId { get; }

    public DateTime BorrowDate { get; }

    public Member Member { get; }

    public LibraryItem Item { get; }

    public LoanStatus Status { get; private set; }

    public DateTime? ReturnDate { get; private set; }

    public DateTime DueDate
    {
        get
        {
            return BorrowDate.AddDays(Item.LoanPeriod);
        }
    }

    public decimal LateFee
    {
        get
        {
            if (ReturnDate == null)
                return 0m;

            if (ReturnDate.Value <= DueDate)
                return 0m;

            TimeSpan lateTime = ReturnDate.Value - DueDate;
            int lateDays = lateTime.Days;

            decimal fee = lateDays * Item.DailyLateFee;
            decimal discount = Member.GetDiscountAmount(fee);
            decimal finalFee = fee - discount;

            if (finalFee < 0)
                return 0m;

            return finalFee;
        }
    }

    public Loan(
        int loanId,
        DateTime borrowDate,
        Member member,
        LibraryItem item)
    {
        if (member == null)
            throw new ArgumentException("Member cannot be null.");

        if (item == null)
            throw new ArgumentException("Item cannot be null.");

        item.CanBeBorrowed();

        LoanId = loanId;
        BorrowDate = borrowDate;
        Member = member;
        Item = item;
        Status = LoanStatus.Borrowed;
        ReturnDate = null;
    }

    public void Return(DateTime returnDate)
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException(
                "Only a borrowed loan can be returned.");

        if (returnDate < BorrowDate)
            throw new ArgumentException(
                "Return date cannot be earlier than borrow date.");

        ReturnDate = returnDate;
        Status = LoanStatus.Returned;
        Item.SetReturned();
    }

    public void MarkAsLost()
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException(
                "Only a borrowed loan can be marked as lost.");

        Status = LoanStatus.Lost;
        Item.SetReturned();
    }
}

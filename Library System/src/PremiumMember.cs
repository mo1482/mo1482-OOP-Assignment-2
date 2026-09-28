namespace LibrarySystem;

public class PremiumMember : Member
{
    public int ReadingPoints
    {
        get
        {
            int points = 0;

            for (int i = 0; i < Loans.Count; i++)
            {
                if (Loans[i].Status == LoanStatus.Returned)
                    points += 5;
            }

            return points;
        }
    }

    public PremiumMember(
        string personId,
        string fullName,
        string phone,
        decimal discountPercentage)
        : base(personId, fullName, phone, 10, discountPercentage)
    {
    }
}

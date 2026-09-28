using LibrarySystem;

Console.WriteLine("=== LIBRARY SYSTEM ===");
Console.WriteLine();

Console.WriteLine("=== 1. Compile-Time Restrictions ===");

// These lines must NOT compile. They are intentionally commented out.

// Person person = new Person("P1", "Ali", "01000000000");

// Member member = new Member(
//     "M1", "Ali", "01000000000", 3, 0);

// Staff staff = new Staff(
//     "S1", "Ahmed", "01100000000",
//     DateTime.Today, 10000, 0);

// LibraryItem item = new LibraryItem(
//     "I1", "Item", 10, 7, 1);

// member.FullName = "New Name";

// member.Loans.Add(...);

// item.IsOnLoan = true;

Console.WriteLine(
    "Restricted direct creation and external modification are prevented.");
Console.WriteLine();

Console.WriteLine("=== 2. Create Members ===");

StudentMember student =
    new StudentMember(
        "M001",
        "Student Member",
        "01011111111");

PremiumMember premium =
    new PremiumMember(
        "M002",
        "Premium Member",
        "01022222222",
        20m);

Console.WriteLine(student.FullName);
Console.WriteLine(premium.FullName);
Console.WriteLine();

Console.WriteLine("=== 3. Create Library Items ===");

Book book = new Book("B001", "Clean Code", 10m);
DVD dvd = new DVD("D001", "C# Fundamentals", 10m);
Magazine magazine = new Magazine("M001", "Technology Monthly", 10m);

Console.WriteLine(
    $"Book: {book.LoanPeriod} days, {book.DailyLateFee} daily fee");

Console.WriteLine(
    $"DVD: {dvd.LoanPeriod} days, {dvd.DailyLateFee} daily fee");

Console.WriteLine(
    $"Magazine: {magazine.LoanPeriod} days, {magazine.DailyLateFee} daily fee");

Console.WriteLine();

Console.WriteLine("=== 4. Borrow Withdrawn Item ===");

book.Withdraw();

try
{
    Loan withdrawnLoan =
        new Loan(
            1,
            new DateTime(2026, 9, 1),
            student,
            book);

    student.Borrow(withdrawnLoan);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

book.Restore();

Console.WriteLine();

Console.WriteLine("=== 5. Borrow Same Item Twice ===");

DateTime borrowDate = new DateTime(2026, 9, 1);

Loan firstLoan =
    new Loan(
        2,
        borrowDate,
        student,
        book);

student.Borrow(firstLoan);

try
{
    PremiumMember secondMember =
        new PremiumMember(
            "M003",
            "Second Member",
            "01033333333",
            10m);

    Loan secondLoan =
        new Loan(
            3,
            borrowDate,
            secondMember,
            book);

    secondMember.Borrow(secondLoan);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine();

Console.WriteLine("=== 6. Student Maximum Loan Limit ===");

Book book2 = new Book("B002", "Book 2", 10m);
Book book3 = new Book("B003", "Book 3", 10m);
Book book4 = new Book("B004", "Book 4", 10m);

Loan studentLoan2 =
    new Loan(4, borrowDate, student, book2);

student.Borrow(studentLoan2);

Loan studentLoan3 =
    new Loan(5, borrowDate, student, book3);

student.Borrow(studentLoan3);

try
{
    Loan studentLoan4 =
        new Loan(6, borrowDate, student, book4);

    student.Borrow(studentLoan4);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine();

Console.WriteLine("=== 7. Staff Monthly Pay ===");

List<Staff> staffMembers = new List<Staff>();

Librarian librarian =
    new Librarian(
        "S001",
        "Librarian",
        "01111111111",
        new DateTime(2025, 1, 1),
        10000m);

HeadLibrarian headLibrarian =
    new HeadLibrarian(
        "S002",
        "Head Librarian",
        "01122222222",
        new DateTime(2024, 1, 1),
        15000m);

Shelver shelver =
    new Shelver(
        "S003",
        "Shelver",
        "01133333333",
        new DateTime(2026, 1, 1),
        7000m,
        "Fiction");

staffMembers.Add(librarian);
staffMembers.Add(headLibrarian);
staffMembers.Add(shelver);

for (int i = 0; i < staffMembers.Count; i++)
{
    Console.WriteLine(
        $"{staffMembers[i].FullName}: " +
        $"{staffMembers[i].GetMonthlyPay()}");
}

Console.WriteLine();

Console.WriteLine("=== 8. Give Raise ===");

Console.WriteLine($"Before raise: {librarian.MonthlySalary}");

librarian.GiveRaise(10);

Console.WriteLine($"After 10% raise: {librarian.MonthlySalary}");

try
{
    librarian.GiveRaise(0);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine();

Console.WriteLine("=== 9. Shelver Reassignment ===");

Console.WriteLine($"Old section: {shelver.Section}");

shelver.Reassign("Children");

Console.WriteLine($"New section: {shelver.Section}");

Console.WriteLine();

Console.WriteLine("=== 10. Premium Late DVD ===");

DateTime premiumBorrowDate =
    new DateTime(2026, 9, 1);

Loan premiumLoan =
    new Loan(
        7,
        premiumBorrowDate,
        premium,
        dvd);

premium.Borrow(premiumLoan);

DateTime returnDate =
    premiumLoan.DueDate.AddDays(5);

premiumLoan.Return(returnDate);

Console.WriteLine($"Due Date: {premiumLoan.DueDate}");
Console.WriteLine($"Return Date: {premiumLoan.ReturnDate}");
Console.WriteLine($"Daily Late Fee: {dvd.DailyLateFee}");
Console.WriteLine($"Late Fee: {premiumLoan.LateFee}");
Console.WriteLine($"Reading Points: {premium.ReadingPoints}");

Console.WriteLine();

Console.WriteLine("=== 11. Return Same Loan Twice ===");

try
{
    premiumLoan.Return(
        premiumLoan.DueDate.AddDays(6));
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine();

Console.WriteLine("=== 12. Mark Returned Loan As Lost ===");

try
{
    premiumLoan.MarkAsLost();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine();

Console.WriteLine("=== 13. LibraryItem Polymorphism ===");

List<LibraryItem> items = new List<LibraryItem>();

items.Add(book);
items.Add(dvd);
items.Add(magazine);

for (int i = 0; i < items.Count; i++)
{
    Console.WriteLine(
        $"{items[i].Title} | " +
        $"Loan Period: {items[i].LoanPeriod} | " +
        $"Daily Fee: {items[i].DailyLateFee}");
}

Console.WriteLine();

Console.WriteLine("=== 14. Raise and Late Fee Validation ===");

try
{
    librarian.GiveRaise(-5);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

try
{
    book.SetLateFee(0);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine();
Console.WriteLine("=== DONE ===");

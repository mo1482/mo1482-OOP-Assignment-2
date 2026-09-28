# Library System — Design With Inheritance

Simulation Academy — .NET Diploma — Cycle 1

## Project Structure

```text
Inheritance/
├── ClassDiagram.mmd
├── README.md
└── src/
    └── LibrarySystem/
        ├── LibrarySystem.csproj
        ├── Program.cs
        ├── Person.cs
        ├── Member.cs
        ├── StudentMember.cs
        ├── PremiumMember.cs
        ├── Staff.cs
        ├── Librarian.cs
        ├── Shelver.cs
        ├── HeadLibrarian.cs
        ├── LibraryItem.cs
        ├── Book.cs
        ├── DVD.cs
        ├── Magazine.cs
        ├── Loan.cs
        └── LoanStatus.cs
```

## Run

From the `Inheritance` folder:

```bash
dotnet build src/LibrarySystem
dotnet run --project src/LibrarySystem
```

## Design

### Person hierarchy

- `Person`
  - `Member`
    - `StudentMember`
    - `PremiumMember`
  - `Staff`
    - `Librarian`
    - `Shelver`
    - `HeadLibrarian`

### Library item hierarchy

- `LibraryItem`
  - `Book`
  - `DVD`
  - `Magazine`

### Loan

`Loan` connects exactly one `Member` with exactly one `LibraryItem`.

## Important Rules Implemented

- Person identity fields are get-only and validated at construction.
- Person, Member, Staff, and LibraryItem use protected constructors.
- Student members have a maximum of 3 active loans.
- Premium members have a maximum of 10 active loans.
- Premium reading points are computed from returned loans.
- Staff salary can only change through `GiveRaise`.
- Shelver section can only change through `Reassign`.
- Head Librarian receives a fixed 400 responsibility allowance.
- Library item late fees can only change through `SetLateFee`.
- Withdrawn items cannot be borrowed.
- Items already on loan cannot be borrowed again.
- Loan status can only move from Borrowed to Returned or Lost.
- Return dates cannot be before borrow dates.
- Due dates and late fees are calculated automatically.
- Book, DVD, and Magazine behavior is configured through constructor values passed with `base(...)`.
- No type checks or switch statements are used for item-specific late fee calculations.
- `Member.Loans` is exposed as `IReadOnlyList<Loan>`.

## Design Diagram

`ClassDiagram.mmd` contains the Mermaid UML class diagram. It can be opened in a Mermaid-compatible editor and exported as PNG/PDF if required by the submission.

namespace LibrarySystem;

public class Person
{
    public string PersonId { get; }
    public string FullName { get; }
    public string Phone { get; }

    protected Person(string personId, string fullName, string phone)
    {
        if (string.IsNullOrEmpty(personId))
            throw new ArgumentException("Person ID cannot be empty.");

        if (string.IsNullOrEmpty(fullName))
            throw new ArgumentException("Full name cannot be empty.");

        if (string.IsNullOrEmpty(phone))
            throw new ArgumentException("Phone cannot be empty.");

        PersonId = personId;
        FullName = fullName;
        Phone = phone;
    }
}

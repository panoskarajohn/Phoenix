using Ardalis.GuardClauses;

namespace Artist.Core.ValueObjects;

public class PersonalInformation
{
    public string Name { get; }
    public string NickName { get; }
    public string LastName { get; }
    public DateTime BirthDate { get; }
    
    private PersonalInformation(string name, string nickName, string lastName, DateTime birthDate)
    {
        Name = name;
        NickName = nickName;
        LastName = lastName;
        BirthDate = birthDate;
    }

    public static PersonalInformation Create(string name, string nickName, string lastName, DateTime birthDate)
    {
        Guard.Against.NullOrEmpty(name, nameof(name));
        Guard.Against.NullOrEmpty(lastName, nameof(lastName));
        Guard.Against.NullOrEmpty(nickName, nameof(nickName));
        Guard.Against.Default(birthDate, nameof(birthDate));
        Guard.Against.AgainstExpression((birth) => birth < DateTime.UtcNow, birthDate,nameof(birthDate));

        return new PersonalInformation(name, nickName, lastName, birthDate);
    }
}
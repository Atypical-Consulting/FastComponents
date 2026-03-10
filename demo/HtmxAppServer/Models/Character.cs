namespace HtmxAppServer.Models;

public record Character(int Id, string FirstName, string LastName, string Movie, int Year, string Description)
{
    public string Name => string.IsNullOrEmpty(LastName) ? FirstName : $"{FirstName} {LastName}";
}

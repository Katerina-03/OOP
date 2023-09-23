using System.Text.RegularExpressions;
using Banks.Exception;

namespace Banks.Clients;

public class PassportData
{
    public static readonly Regex Regex = new Regex(@"^\d{10}");
    private string _name;
    private string _surname;

    public PassportData(string passportNumber, string name, string surname)
    {
        if (string.IsNullOrEmpty(passportNumber))
            throw new ArgumentNullException();
        if (!Regex.IsMatch(passportNumber))
            throw new BankException("Invalid passport number");
        PassportNumber = passportNumber;
        Name = name;
        Surname = surname;
    }

    private string PassportNumber { get; }

    private string Name
    {
        get => _name;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _name = value;
        }
    }

    private string Surname
    {
        get => _surname;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _surname = value;
        }
    }
}
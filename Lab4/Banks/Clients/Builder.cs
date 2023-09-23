namespace Banks.Clients;

public class Builder
{
    private string _passportNumber;
    private string _name;
    private string _surname;

    public Builder SetPassportNumber(string passportNumber)
    {
        ArgumentNullException.ThrowIfNull(passportNumber);
        _passportNumber = passportNumber;
        return this;
    }

    public Builder SetName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        _name = name;
        return this;
    }

    public Builder SetSurname(string surname)
    {
        ArgumentNullException.ThrowIfNull(surname);
        _surname = surname;
        return this;
    }

    public PassportData Create()
    {
        return new PassportData(_passportNumber, _name, _surname);
    }
}
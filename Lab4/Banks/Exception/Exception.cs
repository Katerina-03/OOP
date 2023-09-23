namespace Banks.Exception;

public class BankException : System.Exception
{
    public BankException(string message)
        : base(message)
    {
    }
}
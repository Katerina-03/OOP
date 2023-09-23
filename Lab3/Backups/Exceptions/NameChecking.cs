namespace Backups.Exceptions;

public class NameChecking : Exception
{
    public NameChecking(string pointName)
        : base($"There is not such name")
    {
    }
}
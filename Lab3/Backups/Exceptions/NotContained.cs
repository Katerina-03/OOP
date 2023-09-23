using Backups.Entities;

namespace Backups.Exceptions;

public class NotContained : Exception
{
    public NotContained(RestorePoint point)
        : base($"Can't delete this point")
    {
    }
}
using Backups.Entities;
using Backups.Models;

namespace Backups.Exceptions;

public class NotExist : Exception
{
    public NotExist(BackupObject backupObject, IRepository repository)
        : base($"Can't add this object")
    {
    }
}
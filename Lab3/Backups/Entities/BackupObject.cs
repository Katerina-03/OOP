namespace Backups.Entities;

public class BackupObject
{
    public BackupObject(string path)
    {
        if (string.IsNullOrEmpty(path))
            throw new ArgumentNullException(path);
        Path = path;
    }

    public string Path { get; }
}
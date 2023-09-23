using Backups.Exceptions;
using Backups.Models;

namespace Backups.Entities;

public class RestorePoint
{
    private List<BackupObject> _backupFiles = new ();
    private List<BackupObject> _backupDirectories = new ();

    public RestorePoint(string pointName)
    {
        if (string.IsNullOrEmpty(pointName))
            throw new ArgumentNullException(nameof(pointName));

        CreationData = DateTime.Now;
        PointName = pointName;
    }

    public string PointName { get; }

    public IReadOnlyCollection<BackupObject> BackupFiles => _backupFiles;
    public IReadOnlyCollection<BackupObject> BackupDirectories => _backupDirectories;

    public DateTime CreationData { get; }

    public void AddObject(BackupObject backupObject, IRepository repository)
    {
        ArgumentNullException.ThrowIfNull(backupObject);
        ArgumentNullException.ThrowIfNull(repository);

        if (repository.FileExist(backupObject.Path))
            _backupFiles.Add(backupObject);
        if (repository.DirectoryExist(backupObject.Path))
            _backupDirectories.Add(backupObject);

        if (!repository.FileExist(backupObject.Path) && !repository.DirectoryExist(backupObject.Path))
            throw new NotExist(backupObject, repository);
    }

    public void AddObjects(ICollection<BackupObject> objects, IRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        foreach (var backupObject in objects)
            AddObject(backupObject, repository);
    }

    public void DeleteObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);

        if (_backupFiles.Contains(backupObject))
            _backupFiles.Remove(backupObject);
        if (_backupDirectories.Contains(backupObject))
            _backupDirectories.Remove(backupObject);
    }
}
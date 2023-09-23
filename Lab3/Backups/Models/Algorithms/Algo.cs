using Backups.Entities;
using Backups.Models.Strorage;
using Zio;

namespace Backups.Models.Algorithms;

public class Algo : IAlgo
{
    public Storage Storage { get; } = new ();

    public Storage Save(RestorePoint point, IRepository repository)
    {
        Storage.CreateDirectory($@"\{point.PointName}");
        foreach (var fileObject in point.BackupFiles)
        {
            Storage.CreateFile($@"\{point.PointName}\{Path.GetFileName(fileObject.Path)}");
            string fileData = repository.ReadFile(fileObject.Path);
            Storage.EnterInFile($@"\{point.PointName}\{Path.GetFileName(fileObject.Path)}", fileData);
        }

        return Storage;
    }
}
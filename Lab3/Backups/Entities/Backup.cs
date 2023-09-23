namespace Backups.Entities;

public class Backup
{
    private List<RestorePoint> _points = new ();

    public IReadOnlyCollection<RestorePoint> Points => _points;

    public void AddRestorePont(RestorePoint point)
    {
        ArgumentNullException.ThrowIfNull(point);
        _points.Add(point);
    }

    public void RemoveRestorePoint(RestorePoint point)
    {
        _points.Remove(point);
    }
}
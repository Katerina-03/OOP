using Backups.Entities;
using Backups.Models.Strorage;

namespace Backups.Models.Algorithms;

public interface IAlgo
{
    Storage Save(RestorePoint point, IRepository repository);
}
using Zio;

namespace Backups.Models;

public class Repository : IRepository
{
    public IFileSystem FileSystem { get; set; }

    public void CreateFile(string path)
    {
        using (Stream file = FileSystem.CreateFile(path))
        {
        }
    }

    public void CreateDirectory(string path)
    {
        FileSystem.CreateDirectory(path);
    }

    public void CopyDirectoryAnotherSystem(string copyDirectPath, string insertDirectPath, IFileSystem fileSystem)
    {
        FileSystem.CopyDirectory(copyDirectPath, fileSystem, insertDirectPath, true);
    }

    public void EnterInFile(string filePath, string data)
    {
        FileSystem.WriteAllText(filePath, data);
    }

    public string ReadFile(string filePath)
    {
        return FileSystem.ReadAllText(filePath);
    }

    public bool DirectoryExist(string directoryPath)
    {
        return FileSystem.DirectoryExists(directoryPath);
    }

    public bool FileExist(string filePath)
    {
        return FileSystem.FileExists(filePath);
    }
}
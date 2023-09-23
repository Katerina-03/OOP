using Zio;

namespace Backups.Models;

public interface IRepository
{
    IFileSystem FileSystem { get; }
    void CreateFile(string path);
    void CreateDirectory(string path);
    string ReadFile(string filePath);

    bool DirectoryExist(string directoryPath);

    bool FileExist(string filePath);
    public void CopyDirectoryAnotherSystem(string copyDirectPath, string insertDirectPath, IFileSystem fileSystem);

    void EnterInFile(string filePath, string data);
}
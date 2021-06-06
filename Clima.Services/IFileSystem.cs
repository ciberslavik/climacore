namespace Clima.Services
{
    public interface IFileSystem
    {
        string AppBasePath { get; }
        string DataStoragePath { get; }
        bool FolderExsist(string path);
        bool FileExist(string path);
        void CreateDirectory(string path);
    }
}
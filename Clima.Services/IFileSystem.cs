namespace Clima.Services
{
    public interface IFileSystem
    {
        string AppBasePath { get; }
        string DataStoragePath { get; }
        string ConfigurationPath { get; }
        bool FolderExsist(string path);
        bool FileExist(string path);
        void CreateDirectory(string path);
        void WriteTextFile(string filePath, string data);
    }
}
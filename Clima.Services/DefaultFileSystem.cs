namespace Clima.Services
{
    public class DefaultFileSystem:IFileSystem
    {
        private string _appBasePath;
        private string _dataStoragePath;

        public string AppBasePath => _appBasePath;

        public string DataStoragePath => _dataStoragePath;

        public bool FolderExsist(string path)
        {
            throw new System.NotImplementedException();
        }

        public bool FileExist(string path)
        {
            throw new System.NotImplementedException();
        }

        public void CreateDirectory(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}
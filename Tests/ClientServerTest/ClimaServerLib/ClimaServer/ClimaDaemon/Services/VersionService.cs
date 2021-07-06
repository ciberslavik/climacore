namespace ConsoleServer
{
    public class VersionService
    {
        public object Execute(VersionRequest request)
        {
            return new VersionResponse()
            {
                ProductName = "Clima daemon server",
                ProductVersion = "0.1",
                EngineVersion = "0.0"
            };
        }
    }
}
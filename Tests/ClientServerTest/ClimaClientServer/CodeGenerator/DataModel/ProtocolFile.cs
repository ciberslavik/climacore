namespace CodeGenerator.DataModel
{
    public class ProtocolFile
    {
        public ProtocolFile(string filePath = "")
        {
            FileName = filePath;
        }

        public void ParseFile()
        {
            
        }
        public string FileName { get; set; }
    }
}
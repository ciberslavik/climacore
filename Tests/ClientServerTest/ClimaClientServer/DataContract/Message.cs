namespace DataContract
{
    public class Message
    {
        private string _name;
        private string _data;
        private int _dataLen;

        public Message(string name = "", string data = "")
        {
            Name = name;
            Data = data;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Data
        {
            get => _data;
            set => _data = value;
        }

        public int DataLen
        {
            get => _dataLen;
            set => _dataLen = value;
        }
    }
}
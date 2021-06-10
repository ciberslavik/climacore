namespace DataContract
{
    public interface IDataSerializer
    {
        string Serialize<T>(T value);
        T Deserialize<T>(string data);
    }
}
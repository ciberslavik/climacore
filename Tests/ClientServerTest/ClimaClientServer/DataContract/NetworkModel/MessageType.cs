namespace DataContract.NetworkModel
{
    public enum MessageType:byte
    {
        Unknown = 0,
        DataGet = 1,
        DataUpdate = 2,
        DataSet =3,
        RPC =4
    }
}
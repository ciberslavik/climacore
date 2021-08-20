namespace Clima.Core.IO
{
    public interface IIOServiceFactory
    {
        public IIOService Create(bool debug);
    }
}
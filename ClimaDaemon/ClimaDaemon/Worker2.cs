using Clima.ServiceContainer.CastleWindsor;

namespace ConsoleServer
{
    public class Worker2
    {
        private ApplicationBuilder _builder;

        public Worker2()
        {

        }

        public void Run()
        {
            _builder = new ApplicationBuilder();
            _builder.Initialize(false);
        }
    }
}
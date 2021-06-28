using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.CommandProcessor;
using Clima.GrpcServer;
using Clima.NewtonSoftJsonSerializer;
using Clima.Services.Communication;
using Clima.Services.Configuration;
using Clima.TcpServer.CoreServer;

namespace ClimaD.Installers
{
    public class CommunicationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var configStore = container.Resolve<IConfigurationStorage>();

            if (!configStore.Exist("ServerConfig"))
            {
                var conf = new ServerConfig();
                configStore.RegisterConfig("ServerConfig", conf);
            }
            
            var serverConfig = configStore.GetConfig<ServerConfig>("ServerConfig");

            container.Register(Component
                    .For<ServerConfig>()
                    .Instance(serverConfig),
                Component
                    .For<IServer>()
                    .ImplementedBy<Server>()
                    .LifestyleSingleton(),
                Component
                    .For<ICommunicationSerializer>()
                    .ImplementedBy<NewtonsoftCommunicationSerializer>()
                    .LifestyleSingleton());
        }
    }
}

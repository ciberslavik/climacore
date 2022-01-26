using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Clima.Basics.Configuration;
using Clima.Basics.Services;
using Clima.Basics.Services.Communication;
using Clima.Configuration.FileSystem;
using Clima.Core.Alarm;
using Clima.Core.Hystory;
using Clima.History.MySQL;
using Clima.History.MySQL.Configurations;
using Clima.History.Service;
using Clima.NetworkServer;
using Clima.NetworkServer.Serialization.Newtonsoft;
using Clima.Serialization.Newtonsoft;

namespace Clima.ServiceContainer.CastleWindsor.Installers
{
    public class BasicsInstaller:IWindsorInstaller
    {
        public BasicsInstaller(ISystemLogger logger)
        {
            Log = logger;
        }
        public ISystemLogger Log { get; private set; }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Log.Info("Install basic components");

            container.Register(
                Component
                    .For<IConfigurationSerializer>()
                    .ImplementedBy<ConfigurationSerializer>()
                    .LifestyleSingleton(),
                Component
                    .For<INetworkSerializer>()
                    .ImplementedBy<JsonNetworkSerializer>()
                    .LifestyleSingleton(),
                Component
                    .For<IFileSystem>()
                    .ImplementedBy<DefaultFileSystem>()
                    .LifestyleSingleton(),
                Component
                    .For<IConfigurationStorage>()
                    .ImplementedBy<FSConfigurationStorage>()
                    .LifestyleSingleton());
            
            HistoryMySQLConfig sqlConfig;
            var configStore = container.Resolve<IConfigurationStorage>();
            
            if (configStore.Exist<HistoryMySQLConfig>())
            {
                sqlConfig = configStore.GetConfig<HistoryMySQLConfig>();
            }
            else
            {
                sqlConfig = HistoryMySQLConfig.CreateDefault(); 
                configStore.RegisterConfig(sqlConfig);
            }
            
            var sqlHistory = new MyClient(sqlConfig);
            
            sqlHistory.AddBootRecord();

            container.Register(
                Component
                    .For<IHistoryService>()
                    .Instance(sqlHistory)
                    .LifestyleSingleton(),
                Component
                    .For<IAlarmManager>()
                    .ImplementedBy<AlarmManager>()
                    .LifestyleSingleton());
        }
    }
}
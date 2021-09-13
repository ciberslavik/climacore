namespace Clima.Core.Scheduler.Network.Messages
{
    public class ProductionConfigRequest
    {
        public ProductionConfigRequest()
        {
            
        }

        public ProductionConfig Config { get; set; } = new ProductionConfig();
    }
}
using Clima.Core.DataModel.History;

namespace Clima.Core.Hystory
{
    public interface IHistoryService
    {
        void AddClimatPoint(ClimatStateHystoryItem point);
        void AddBootRecord();
    }
}
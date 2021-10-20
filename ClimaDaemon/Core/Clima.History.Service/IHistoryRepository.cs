using Clima.Core.DataModel.History;

namespace Clima.History.Service
{
    public interface IHistoryRepository
    {
        event RepoStateChangedEventHandler StateChanged;
        void AddClimatePoint(ClimatStateHystoryItem point);
    }
}
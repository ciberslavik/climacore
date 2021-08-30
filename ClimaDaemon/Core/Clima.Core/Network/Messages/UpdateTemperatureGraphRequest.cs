using System.Collections.Generic;
using Clima.Basics.Services.Communication;
using Clima.Core.DataModel.GraphModel;

namespace Clima.Core.Network.Messages
{
    public class UpdateTemperatureGraphRequest:IReturn<DefaultResponse>
    {
        public UpdateTemperatureGraphRequest()
        {
            
        }

        public ValueByDayProfile Profile { get; set; } = new ValueByDayProfile();

    }

    public class ValueByDayProfile
    {
        public ProfileInfo Info { get; set; } = new ProfileInfo();
        public List<ValueByDayPoint> Points { get; set; } = new List<ValueByDayPoint>();
    }
}
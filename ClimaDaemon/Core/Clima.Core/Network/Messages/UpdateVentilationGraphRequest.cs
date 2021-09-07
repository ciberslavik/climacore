using System.Collections.Generic;
using Clima.Basics.Services.Communication;
using Clima.Core.DataModel.GraphModel;

namespace Clima.Core.Network.Messages
{
    public class UpdateVentilationGraphRequest:IReturn<DefaultResponse>
    {
        public UpdateVentilationGraphRequest()
        {
        }

        public MinMaxByDayProfile Profile { get; set; } = new MinMaxByDayProfile();
    }

    public class MinMaxByDayProfile
    {
        public MinMaxByDayProfile()
        {
            
        }

        public ProfileInfo Info { get; set; } = new ProfileInfo();
        public List<MinMaxByDayPoint> Points { get; set; } = new List<MinMaxByDayPoint>();
    }
}
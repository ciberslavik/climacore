using System.Collections.Generic;
using Clima.Core.DataModel.GraphModel;

namespace Clima.Core.Network.Messages
{
    public class UpdateValveGraphRequest
    {
        public UpdateValveGraphRequest()
        {
            
        }

        public ValueByValueProfile Profile { get; set; } = new ValueByValueProfile();
    }

    public class ValueByValueProfile
    {
        public ValueByValueProfile()
        {
            
        }

        public ProfileInfo Info { get; set; } = new ProfileInfo();
        public List<ValueByValuePoint> Points { get; set; } = new List<ValueByValuePoint>();
    }
}
using System.Collections.Generic;
using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Network.Messages
{
    public class HeaterParamsRequest
    {
        public HeaterParamsRequest()
        {
        }

        public List<HeaterParams> ParamsList { get; set; } = new List<HeaterParams>();
    }
}
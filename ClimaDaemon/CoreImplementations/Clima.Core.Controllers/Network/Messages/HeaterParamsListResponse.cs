using System.Collections.Generic;
using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Network.Messages
{
    public class HeaterParamsListResponse
    {
        public List<HeaterParams> ParamsList { get; set; } = new List<HeaterParams>();
    }
}
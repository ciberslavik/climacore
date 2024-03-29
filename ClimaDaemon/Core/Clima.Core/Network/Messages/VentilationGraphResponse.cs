﻿using System.Collections.Generic;
using Clima.Core.DataModel.GraphModel;

namespace Clima.Core.Network.Messages
{
    public class VentilationGraphResponse
    {
        public VentilationGraphResponse()
        {
            
        }
        public ProfileInfo Info { get; set; } = new ProfileInfo();
        public List<MinMaxByDayPoint> Points { get; set; } = new List<MinMaxByDayPoint>();
    }
}
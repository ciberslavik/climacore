﻿using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Network.Messages
{
    public class FanStateRequest
    {
        public string Key { get; set; }
        public FanStateEnum State { get; set; }
        public float AnalogPower { get; set; }
    }
}
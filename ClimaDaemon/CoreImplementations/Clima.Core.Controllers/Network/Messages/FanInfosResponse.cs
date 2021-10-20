﻿using System.Collections.Generic;
using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Network.Messages
{
    public class FanInfosResponse
    {
        public FanInfosResponse()
        {
        }

        public Dictionary<string, FanInfo> Infos { get; set; } = new Dictionary<string, FanInfo>();
    }
}
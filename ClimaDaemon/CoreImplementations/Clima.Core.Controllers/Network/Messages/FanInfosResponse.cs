﻿using System.Collections.Generic;
using Clima.Core.DataModel;

namespace Clima.Core.Controllers.Network.Messages
{
    public class FanInfosResponse
    {
        public FanInfosResponse()
        {
        }

        public List<FanInfo> Infos { get; set; } = new List<FanInfo>();
    }
}
using System;

namespace Clima.Core
{
    public class DefaultTimeProvider:ITimeProvider
    {
        
        public DateTime Now => DateTime.Now;
    }
}
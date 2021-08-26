using System;

namespace Clima.Core
{
    public interface ITimeProvider
    {
        DateTime Now { get; }
    }
}
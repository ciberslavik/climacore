using System;

namespace Clima.FSGrapRepository.Configuration
{
    public interface IGraphPointConfig<TPoint> : IComparable<IGraphPointConfig<TPoint>>
    {
        int Index { get; }
    }
}
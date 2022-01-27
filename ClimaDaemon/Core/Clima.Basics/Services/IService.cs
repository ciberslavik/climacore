using System;

namespace Clima.Basics.Services
{
    public interface IService
    {
        void Start();
        void Stop();
        void Cycle();
        void Init(object config);
        Type ConfigType { get; }
        ServiceState ServiceState { get; }
    }
}
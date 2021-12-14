using System;
using Clima.Basics.Configuration;

namespace Clima.Core.Controllers
{
    public interface IController
    {
        void Start();
        void Stop();
        void Initialize(object cnfig);
        void ReloadConfig(object cnfig);
        void Process(object? context);
        Type ConfigurationType { get; }
    }
}
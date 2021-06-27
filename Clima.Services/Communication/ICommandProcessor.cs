using System;

namespace Clima.Services.Communication
{
    public interface ICommandProcessor
    {
        public void ProcessCommand(Guid sessinId, string data);
    }
}
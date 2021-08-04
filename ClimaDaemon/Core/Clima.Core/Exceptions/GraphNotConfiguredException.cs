using System;

namespace Clima.Core.Exceptions
{
    public class GraphNotConfiguredException:Exception
    {
        private string _propertyName;
        private string _messageInternal;
        public GraphNotConfiguredException(string propertyName="",string message=""):base(message)
        {
            _propertyName = propertyName;
        }


        public string PropertyName
        {
            get => _propertyName;
            set => _propertyName = value;
        }

        public string MessageInternal
        {
            get => _messageInternal;
            set => _messageInternal = value;
        }
    }
}
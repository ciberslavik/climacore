using System.Collections.Generic;

namespace ProtocolGenerator.DataModel
{
    public class Method
    {
        public Method()
        {
            Parameters = new List<MethodParameter>();
        }

        public string MethodName { get; set; }
        public string ReturnType { get; set; }
        public List<MethodParameter> Parameters { get; set; }
    }
}
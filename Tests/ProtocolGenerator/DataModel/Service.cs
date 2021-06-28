using System.Collections.Generic;

namespace ProtocolGenerator.DataModel
{
    public class Service
    {
        public Service()
        {
            Methods = new List<Method>();
            Properties = new List<Property>();
        }
        public string ServiceName { get; set; }
        public List<Method> Methods { get; set; }
        public List<Property> Properties { get; set; }
    }
}
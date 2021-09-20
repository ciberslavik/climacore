using System;

namespace Clima.Core.IO.Converters
{
    public class VoltageToPercentConverter : IAnalogValueConverter
    {
        public VoltageToPercentConverter()
        {
        }


        public double ConvertTo(ushort[] value)
        {
            double va = value[0];

            //Console.WriteLine($"VoLTAGE value :{va}");

            return va/10;
        }

        public ushort[] ConvertFrom(double value)
        {
            return new ushort[2];
        }
    }
}
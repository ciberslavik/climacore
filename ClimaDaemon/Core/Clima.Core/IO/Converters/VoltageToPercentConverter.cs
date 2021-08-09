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

            //Console.WriteLine($"Voltage value:{va/100}");

            return va / 100;
        }

        public ushort[] ConvertFrom(double value)
        {
            return new ushort[2];
        }
    }
}
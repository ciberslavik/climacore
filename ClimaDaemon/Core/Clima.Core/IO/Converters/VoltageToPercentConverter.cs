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
            var v = (ushort)value[1] << 16 | (ushort)value[0];
            
            double va = v / 10f;

            return va;
        }

        public ushort[] ConvertFrom(double value)
        {
            var v = (int)(value * 10);
            var buff = new ushort[2];
            buff[1] = (ushort) (v >> 16);
            buff[0] = (ushort) (v & 0xffff);

            return buff;
        }
    }
}
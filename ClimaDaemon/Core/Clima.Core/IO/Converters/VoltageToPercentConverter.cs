namespace Clima.Core.IO.Converters
{
    public class VoltageToPercentConverter:IAnalogValueConverter
    {
        public VoltageToPercentConverter()
        {
        }


        public double ConvertTo(double value)
        {
            return value / 100;
        }

        public double ConvertFrom(double value)
        {
            return value;
        }
    }
}
namespace Clima.Core.IO
{
    public interface IAnalogValueConverter
    {
        double ConvertTo(ushort[] value);
        ushort[] ConvertFrom(double value);
    }
}
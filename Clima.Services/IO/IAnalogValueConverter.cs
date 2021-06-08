namespace Clima.Services.IO
{
    public interface IAnalogValueConverter
    {
        double ConvertTo(double value);
        double ConvertFrom(double value);
    }
}
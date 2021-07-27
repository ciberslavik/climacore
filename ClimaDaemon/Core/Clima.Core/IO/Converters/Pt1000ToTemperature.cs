using System;

namespace Clima.Core.IO.Converters
{
    public class Pt1000ToTemperature:IAnalogValueConverter
    {
        public Pt1000ToTemperature()
        {
        }


        public double ConvertTo(double value)
        {
            double t;

            double A = 3.9083e-3;
            double B = -5.775e-7;
            double D1 = 255.819;
            double D2 = 9.14550;
            double D3 = -2.92363;
            double D4 = 1.79090;
            double Ro = 1000.12;

            if ((value / Ro) >= 1.0)
            {
                t = (Math.Sqrt((Math.Pow(A, 2) - ((1.0 - (value / Ro)) *
                                                  0.0004 * B))) - A) / (0.0002 * B);
            }

            else
            {
                t = D1 * (((value * A) / Ro) - 1.0) +
                    D2 * Math.Pow(((value * A) / Ro) - 1.0, 2) +
                    D3 * Math.Pow((((value * A) / Ro) - 1.0), 3) +
                    D4 * Math.Pow((((value * A) / Ro) - 1.0), 4);
            }

            return t;
        }

        public double ConvertFrom(double value)
        {
            throw new System.NotImplementedException();
        }
    }
}
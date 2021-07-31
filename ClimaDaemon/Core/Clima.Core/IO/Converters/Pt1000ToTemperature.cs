using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Clima.Core.IO.Converters
{
    [StructLayout(LayoutKind.Explicit)]
    public struct UInt32ToFloat
    {
        [FieldOffset(0)]
        public uint UInt32;

        [FieldOffset(0)]
        public float Single;
    }
    public class Pt1000ToTemperature:IAnalogValueConverter
    {
        public double ConvertTo(ushort[] value)
        {
            //Console.WriteLine($"PT1000 value:{value:F4}");
            if (value.Length >= 2)
            {
                Console.WriteLine($"vv:{value[0]} {value[1]}");
            }

            float va = (float)value[0] ;

            //Console.Write($"PT1000 raw value:{va} ");

            double v = va;
            double t;

            double A = 3.9083e-3;
            double B = -5.775e-7;
            double D1 = 255.819;
            double D2 = 9.14550;
            double D3 = -2.92363;
            double D4 = 1.79090;
            double Ro = 1000.12;

            if ((v / Ro) >= 1.0)
            {
                t = (Math.Sqrt((Math.Pow(A, 2) - ((1.0 - (v / Ro)) *
                                                  0.0004 * B))) - A) / (0.0002 * B);
            }

            else
            {
                t = D1 * (((v * A) / Ro) - 1.0) +
                    D2 * Math.Pow(((v * A) / Ro) - 1.0, 2) +
                    D3 * Math.Pow((((v * A) / Ro) - 1.0), 3) +
                    D4 * Math.Pow((((v * A) / Ro) - 1.0), 4);
            }
            //Console.WriteLine($" temperature:{t}");
            return t;
        }

        public ushort[] ConvertFrom(double value)
        {
            throw new System.NotImplementedException();
        }
        
        public static float FromFloatSafe(uint fb)
        {
            

            int sign = (int)((fb >> 31) & 1);
            int exponent = (int)((fb >> 23) & 0xFF);
            int mantissa = (int)(fb & 0x7FFFFF);

            float fMantissa;
            float fSign = sign == 0 ? 1.0f : -1.0f;

            if (exponent != 0)
            {
                exponent -= 127;
                fMantissa = 1.0f + (mantissa / (float)0x800000);
            }
            else
            {
                if (mantissa != 0)
                {
                    // denormal
                    exponent -= 126;
                    fMantissa = 1.0f / (float)0x800000;
                }
                else
                {
                    // +0 and -0 cases
                    fMantissa = 0;
                }
            }

            float fExponent = (float)Math.Pow(2.0, exponent);
            float ret = fSign * fMantissa * fExponent;
            return ret;
        }

    }
}
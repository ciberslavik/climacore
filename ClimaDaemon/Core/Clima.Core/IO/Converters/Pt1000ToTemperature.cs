using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Clima.Core.IO.Converters
{
    [StructLayout(LayoutKind.Explicit)]
    public struct UInt32ToFloat
    {
        [FieldOffset(0)] public uint UInt32;

        [FieldOffset(0)] public float Single;
    }

    public class Pt1000ToTemperature : IAnalogValueConverter
    {
        public double ConvertTo(ushort[] value)
        {
            //Console.WriteLine($"PT1000 value:{value:F4}");
            var va = (ushort)value[1] << 16 | (ushort)value[0];
            
            double v = (float)va / 100f;
            double t = 0;

            var A = 3.9083e-3;
            var B = -5.775e-7;
            /*var D1 = 255.819;
            var D2 = 9.14550;
            var D3 = -2.92363;
            var D4 = 1.79090;*/
            var Ro = 1000.12;

            //if (v / Ro >= 1.0)
            //{
                t = (Math.Sqrt(Math.Pow(A, 2) - 4 * B * (1.0 - v / Ro)) - A) / (2 * B);
                
            /*}
            else
            {
                t = D1 * (v * A / Ro - 1.0) +
                    D2 * Math.Pow(v / Ro - 1.0, 2) +
                    D3 * Math.Pow(v / Ro - 1.0, 3) +
                    D4 * Math.Pow(v / Ro - 1.0, 4);
                Console.WriteLine($" temperature:{t}");
            }*/

            return t;
        }

        public ushort[] ConvertFrom(double value)
        {
            throw new NotImplementedException();
        }

        public static float FromFloatSafe(uint fb)
        {
            var sign = (int) ((fb >> 31) & 1);
            var exponent = (int) ((fb >> 23) & 0xFF);
            var mantissa = (int) (fb & 0x7FFFFF);

            float fMantissa;
            var fSign = sign == 0 ? 1.0f : -1.0f;

            if (exponent != 0)
            {
                exponent -= 127;
                fMantissa = 1.0f + mantissa / (float) 0x800000;
            }
            else
            {
                if (mantissa != 0)
                {
                    // denormal
                    exponent -= 126;
                    fMantissa = 1.0f / (float) 0x800000;
                }
                else
                {
                    // +0 and -0 cases
                    fMantissa = 0;
                }
            }

            var fExponent = (float) Math.Pow(2.0, exponent);
            var ret = fSign * fMantissa * fExponent;
            return ret;
        }
    }
}
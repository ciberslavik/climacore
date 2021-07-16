using System;

namespace Clima.Basics
{
    public static class MathUtils
    {
        public static float Clamp01(float value)
        {
            if (value < 0F)
                return 0F;
            else if (value > 1F)
                return 1F;
            else
                return value;
        }

        // Interpolates between /a/ and /b/ by /t/. /t/ is clamped between 0 and 1.
        public static float Lerp(float start, float end, float x)
        {
            return start + (end - start) * Clamp01(x);
        }
    }
}
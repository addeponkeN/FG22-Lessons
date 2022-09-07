using System;

namespace Util
{
    public static class Easings
    {
        public delegate float Interpolator(float x);
        public static float ExpoOut(float x) => 1f - (float)Math.Pow(2f, -8f * x);
    }
}
namespace SnappyMap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Util
    {
        public static int Clamp(int val, int min, int max)
        {
            return Math.Min(max, Math.Max(val, min));
        }
    }
}

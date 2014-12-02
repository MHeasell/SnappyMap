namespace SnappyMap
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class Util
    {
        public static string GetVersion()
        {
            var attrib = (AssemblyInformationalVersionAttribute)
                Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), true)
                    .Single();
            return attrib.InformationalVersion;
        }

        public static int Clamp(int val, int min, int max)
        {
            return Math.Min(max, Math.Max(val, min));
        }
    }
}

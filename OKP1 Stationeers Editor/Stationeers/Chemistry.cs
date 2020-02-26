using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKP1_Stationeers_Editor.Stationeers
{
    public static class Chemistry
    {
        public enum GasType
        {
            Undefined = 0,
            Oxygen = 1,
            Nitrogen = 2,
            CarbonDioxide = 4,
            Volatiles = 8,
            Pollutant = 16,
            Water = 32,
            NitrousOxide = 64
        }
        public static class Temperature
        {
            public static readonly float ZeroDegrees = 273.15f;
            public static readonly float OneDegree = Chemistry.Temperature.ZeroDegrees + 1f;
            public static readonly float TwentyDegrees = Chemistry.Temperature.ZeroDegrees + 20f;
            public static readonly float Minimum = 1f;
            public static readonly float Maximum = 80000f;
        }
    }
}

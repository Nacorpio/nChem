using System;
using System.Collections.Generic;
using System.Linq;

namespace nChem.Units
{
    public static partial class UnitRepository
    {
        public delegate float UnitFunction(float value);

        public static Prefix[] Prefixes =
        {
            new Prefix("none", ' ', 1e-0f, PrefixKind.None),
            new Prefix("deci", 'd', 1e-1f, PrefixKind.Deci),
            new Prefix("centi", 'c', 1e-2f, PrefixKind.Centi),
            new Prefix("milli", 'm', 1e-3f, PrefixKind.Milli),
            new Prefix("micro", 'µ', 1e-6f, PrefixKind.Micro),
            new Prefix("nano", 'n', 1e-9f, PrefixKind.Nano),
            new Prefix("hecto", 'h', 1e2f, PrefixKind.Hekto),
            new Prefix("kilo", 'k', 1e3f, PrefixKind.Kilo),
            new Prefix("mega", 'M', 1e6f, PrefixKind.Mega),
            new Prefix("giga", 'G', 1e9f, PrefixKind.Giga),
            new Prefix("tera", 'T', 1e12f, PrefixKind.Tera),
            new Prefix("peta", 'P', 1e15f, PrefixKind.Peta),
            new Prefix("exa", 'E', 1e18f, PrefixKind.Exa),
            new Prefix("zetta", 'Z', 1e21f, PrefixKind.Zetta),
            new Prefix("yotta", 'Y', 1e24f, PrefixKind.Yotta),
        };

        public static Dictionary<UnitKind, Dictionary<UnitKind, UnitFunction>> Converters = new Dictionary
            <UnitKind, Dictionary<UnitKind, UnitFunction>>
            {
                {
                    UnitKind.Newton, new Dictionary<UnitKind, UnitFunction>
                    {
                        {UnitKind.Newton, f => f},
                        {UnitKind.Joule, f => 1*f}
                    }
                },

                {
                    UnitKind.Joule, new Dictionary<UnitKind, UnitFunction>
                    {
                        {UnitKind.Joule, f => f},
                        {UnitKind.Newton, f => 1*f},
                        {UnitKind.Calorie, f => 0.239f*f},
                        {UnitKind.Btu, f => 0.0009f*f}
                    }
                },

                {
                    UnitKind.Watt, new Dictionary<UnitKind, UnitFunction>
                    {
                        {UnitKind.HorsePowerMetric, f => 0.0014f*f}
                    }
                },

                {
                    UnitKind.Meter, new Dictionary<UnitKind, UnitFunction>
                    {
                        {UnitKind.Meter, f => 1 * f},
                        {UnitKind.Inch, f => 39.3701f*f},
                        {UnitKind.Mile, f => 0.0006f*f},
                        {UnitKind.Yard, f => 1.0936f*f},
                        {UnitKind.Feet, f => 3.2808f*f},
                        {UnitKind.NauticalMile, f => 0.0005f*f},
                        {UnitKind.Furlong, f => 0.005f*f},
                        {UnitKind.LightYear, f => 9.4605284e15f*f}
                    }
                }
            };

        /// <summary>
        /// Converts a value of a specific unit to the specified target unit.
        /// </summary>
        /// <param name="source">The source unit value.</param>
        /// <param name="targetUnit">The target unit.</param>
        /// <param name="targetPrefix">The target prefix.</param>
        /// <returns></returns>
        public static decimal Calculate(UnitValue source, UnitKind targetUnit, PrefixKind targetPrefix = PrefixKind.None)
        {
            if (!CanConvert(source.Unit, targetUnit))
                throw new Exception("The specified source unit cannot be converted to the target unit.");

            Prefix prefix = Prefixes
                .FirstOrDefault(x => x.Kind == targetPrefix) != null
                    ? Prefixes.First(x => x.Kind == targetPrefix)
                    : Prefixes.First(x => x.Kind == PrefixKind.None);

            return (decimal) Converters[source.Unit][targetUnit](source.GetValue()) / (decimal) prefix.Multiplier;
        }

        /// <summary>
        /// Determines whether the specified unit can be converted to a specific target unit.
        /// </summary>
        /// <param name="source">The source unit.</param>
        /// <param name="target">The target unit.</param>
        /// <returns></returns>
        public static bool CanConvert(UnitKind source, UnitKind target)
        {
            if (!Converters.ContainsKey(source))
                throw new KeyNotFoundException(nameof(source));

            return Converters[source].ContainsKey(target);
        }
    }
}
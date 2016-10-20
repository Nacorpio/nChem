using System.Collections.Generic;

namespace nChem.Units
{
    public static class UnitRepository
    {
        public static Dictionary<UnitType, BaseUnit> Units;

        static UnitRepository()
        {
            Units = new Dictionary<UnitType, BaseUnit>
            {
                {
                    UnitType.Newton,
                    new BaseUnit("N")
                },
            };

            Units.Add(UnitType.Meter, new BaseUnit("m")
            {
                Converters = new Dictionary<UnitType, BaseUnit.UnitCalculatationDelegate>
                {
                    {
                        UnitType.Meter,
                        (x, y) => MathUtils.ToFloat(y) * x
                    },
                }
            });

            Units.Add(UnitType.Inch, new BaseUnit("in")
            {
                Converters = new Dictionary<UnitType, BaseUnit.UnitCalculatationDelegate>
                {
                    {
                        UnitType.Inch,
                        (x, y) => MathUtils.ToFloat(y) * x
                    },
                    {
                        UnitType.Meter,
                        (x, y) => MathUtils.ToFloat(y) * x * 39.3700787f
                    }
                }
            });

            Units.Add(UnitType.Minute, new BaseUnit("min")
            {
                Converters = new Dictionary<UnitType, BaseUnit.UnitCalculatationDelegate>
                {
                    {
                        UnitType.Hour,
                        (x, y) => MathUtils.ToFloat(y) * x / 60
                    },
                    {
                        UnitType.Second,
                        (x, y) => MathUtils.ToFloat(y) * x * 60
                    },
                    {
                        UnitType.Millisecond,
                        (x, y) => Units[UnitType.Second].ConvertTo(x * 60, UnitType.Millisecond, y)
                    }
                }
            });

            Units.Add(UnitType.Hour, new BaseUnit("h")
            {
                Converters = new Dictionary<UnitType, BaseUnit.UnitCalculatationDelegate>
                {
                    {
                        UnitType.Second,
                        (x, y) => MathUtils.ToFloat(y) * x * 3.6e3f
                    },
                    {
                        UnitType.Minute,
                        (x, y) => MathUtils.ToFloat(y) * x * 60
                    },
                    {
                        UnitType.Millisecond,
                        (x, y) => MathUtils.ToFloat(y) * x * 3.6e3f * 1e3f
                    }
                }
            });

            Units.Add(UnitType.Second, new BaseUnit("s")
            {
                Converters = new Dictionary<UnitType, BaseUnit.UnitCalculatationDelegate>
                {
                    {
                        UnitType.Millisecond,
                        (x, y) => MathUtils.ToFloat(y) * x * 1e3f
                    },
                    {
                        UnitType.Hour,
                        (x, y) => MathUtils.ToFloat(y) * x / 3.6e3f
                    },
                    {
                        UnitType.Minute,
                        (x, y) => MathUtils.ToFloat(y) * x / 60
                    }
                }
            });
        }

        public static BaseUnit Newton => Get(UnitType.Newton);
        public static BaseUnit Meter => Get(UnitType.Meter);
        public static BaseUnit Inch => Get(UnitType.Inch);
        public static BaseUnit Second => Get(UnitType.Second);
        public static BaseUnit Minute => Get(UnitType.Minute);
        public static BaseUnit Millisecond => Get(UnitType.Millisecond);
        public static BaseUnit Hour => Get(UnitType.Hour);

        /// <summary>
        /// Returns a <see cref="BaseUnit"/> with the specified unit type.
        /// </summary>
        /// <param name="unitType">The unit type.</param>
        /// <returns></returns>
        public static BaseUnit Get(UnitType unitType) => Units[unitType];
    }
}
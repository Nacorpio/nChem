using nChem.Chemistry;

namespace nChem
{
    public enum PrefixKind : long
    {
        None,
        Deka,
        Hekto,
        Kilo,
        Mega,
        Giga,
        Tera,
        Peta,
        Exa,
        Deci,
        Centi,
        Milli,
        Mikro ,
        Nano,
        Piko,
        Femto,
        Atto,
        Zepto,
        Micro,
        Zetta,
        Yotta
    }

    public enum Magnetism
    {
        Paramagnetic,
        Diamagnetic,
    }

    public enum UnitKind
    {
        Newton,
        Joule,
        Calorie,
        HorsePowerMetric,
        HorsePowerMechanical,
        Watt,
        Btu,

        Second,
        Minute,
        Hour,
        Day,
        Week,

        Inch,
        Mile,
        Yard,
        Feet,
        Furlong,
        NauticalMile,
        LightYear,
        Meter,
    }

    /// <summary>
    /// Represents an electron spin.
    /// </summary>
    public enum ElectronSpin
    {
        Up,
        Down
    }

    /// <summary>
    /// Represents an SI unit.
    /// </summary>
    public enum SiUnit
    {
        Meter,
        Second,
        Ampere,
        Kelvin,
        Mole,
        Candela,
    }

    /// <summary>
    /// Represents an <see cref="Chemistry.Element"/> category.
    /// </summary>
    public enum ElementCategory
    {
        AlkaliMetals,
        AlkalineEarthMetals,
        Pnictogens,
        Chalcogens,
        Halogens,
        NobleGases,
        Lanthanoids,
        Actinoids,
        RareEarthMetals,
        TransitionMetals,
        DiatomicNonMetal
    }
}
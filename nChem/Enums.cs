namespace nChem
{
    public enum Prefix : long
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
    }

    public enum Magnetism
    {
        Paramagnetic,
        Diamagnetic,
    }

    public enum UnitType
    {
        Newton,
        Millisecond,
        Second,
        Minute,
        Hour,
        Joule,
        Calorie,
        Meter,
        Inch
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
    /// Represents an <see cref="Element"/> category.
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
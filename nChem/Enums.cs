namespace nChem
{
    /// <summary>
    /// Represents an SI prefix.
    /// </summary>
    public enum SiPrefix
    {
        Micro = 'µ',
        Milli = 'm',
        Kilo = 'k',
        Deci = 'd',
        Centi = 'c',
        Terra = 'T',
        Giga = 'G',
        Mega = 'M',
        Yotta = 'Y',
        Zetta = 'Z',
        Exa = 'e',
        Peta = 'p',
        Nano = 'n',
        Pico = 'p',
        Femto = 'f',
        Atto = 'a',
        Zepto = 'z',
        Yocto = 'y',
        None,
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
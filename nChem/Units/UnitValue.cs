using System.Linq;

namespace nChem.Units
{
    public sealed class UnitValue
    {
        /// <summary>
        /// Initializes an instance of the <see cref="UnitValue"/> class.
        /// </summary>
        /// <param name="baseValue">The base value.</param>
        /// <param name="unit">The unit kind.</param>
        /// <param name="prefix">The prefix.</param>
        public UnitValue(float baseValue, UnitKind unit, PrefixKind prefix)
        {
            BaseValue = baseValue;
            Prefix = prefix;
            Unit = unit;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="UnitValue"/> class.
        /// </summary>
        /// <param name="baseValue">The base value.</param>
        /// <param name="unit">The unit kind.</param>
        public UnitValue(float baseValue, UnitKind unit)
            : this(baseValue, unit, PrefixKind.None)
        { }

        /// <summary>
        /// Gets the base value of the <see cref="UnitValue"/>.
        /// </summary>
        public float BaseValue { get; }

        /// <summary>
        /// Gets the unit kind of the <see cref="UnitValue"/>.
        /// </summary>
        public UnitKind Unit { get; }

        /// <summary>
        /// Gets the prefix of the <see cref="UnitValue"/>.
        /// </summary>
        public PrefixKind Prefix { get; }

        /// <summary>
        /// Returns the prefix-included value of the <see cref="UnitRepository"/>.
        /// </summary>
        /// <returns></returns>
        public float GetValue()
        {
            var prefix = UnitRepository.Prefixes
                             .FirstOrDefault(x => x.Kind == Prefix) != null
                ? UnitRepository.Prefixes.First(x => x.Kind == Prefix)
                : UnitRepository.Prefixes.First(x => x.Kind == PrefixKind.None);

            return BaseValue*prefix.Multiplier;
        }
    }
}
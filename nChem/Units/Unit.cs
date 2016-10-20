using System.Collections.Generic;

namespace nChem.Units
{
    public class BaseUnit
    {
        public delegate float UnitCalculatationDelegate(float x, Prefix y = Prefix.None);
        
        /// <summary>
        /// Initializes an instance of the <see cref="BaseUnit"/> class.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        internal BaseUnit(string symbol)
        {
            Converters = new Dictionary<UnitType, UnitCalculatationDelegate>();
            Symbol = symbol;
        }

        /// <summary>
        /// Gets the symbol of the <see cref="BaseUnit"/>.
        /// </summary>
        public string Symbol { get; }

        /// <summary>
        /// Gets or sets the base value of the <see cref="BaseUnit"/>.
        /// </summary>
        public float BaseValue { get; set; }

        /// <summary>
        /// Gets the converters of the <see cref="BaseUnit"/>.
        /// </summary>
        public Dictionary<UnitType, UnitCalculatationDelegate> Converters { get; set; }
        
        /// <summary>
        /// Converts the specified value to a specific <see cref="UnitType"/>.
        /// </summary>
        /// <param name="x">The value to convert.</param>
        /// <param name="targetUnit">The target unit.</param>
        /// <param name="targetPrefix">The target prefix.</param>
        /// <returns></returns>
        public float ConvertTo(float x, UnitType targetUnit, Prefix targetPrefix = Prefix.None)
        {
            return Converters[targetUnit](x, targetPrefix);
        }
        
        /// <summary>
        /// Returns the value of the <see cref="BaseUnit"/>.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <returns></returns>
        public float GetValue(Prefix prefix)
        {
            return (long) prefix*BaseValue;
        }
    }
}

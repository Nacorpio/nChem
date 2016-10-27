using System;
using System.Collections.Generic;

namespace nChem.Units
{
    public class Unit
    {
        /// <summary>
        /// Initializes an instance of the <see cref="Unit"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="symbol">The symbol.</param>
        internal Unit(string name, string symbol)
        {
            Name = name;
            Symbol = symbol;
            Converters = new Dictionary<string, Func<float, UnitRepository.Prefix, float>>();
        }

        /// <summary>
        /// Gets the name of the <see cref="Unit"/>.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the symbol of the <see cref="Unit"/>.
        /// </summary>
        public string Symbol { get; }

        /// <summary>
        /// Gets the converters of the <see cref="Unit"/>.
        /// </summary>
        public Dictionary<string, Func<float, UnitRepository.Prefix, float>> Converters { get; }
    }
}

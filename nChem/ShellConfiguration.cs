using System;
using System.Collections.Generic;
using System.Linq;

namespace nChem
{
    /// <summary>
    /// Represents a configuration for the electrons within an <see cref="Atom"/>.
    /// </summary>
    public sealed class ShellConfiguration : List<Shell>
    {
        /// <summary>
        /// Initializes an instance of the <see cref="ShellConfiguration"/> class.
        /// </summary>
        /// <param name="values">An ordered collection of shells.</param>
        public ShellConfiguration(int[] values)
        {
            for (var i = 0; i < values.Length; i++)
            {
                int value = values[i];

                if (value > ChemistryUtils.GetShellCapacity(i) || value < 0)
                    throw new IndexOutOfRangeException(nameof(value));

                Add(new Shell(ChemistryUtils.ShellLabels[i], value));
            }
        }

        /// <summary>
        /// Initializes an instance of the <see cref="ShellConfiguration"/> class.
        /// </summary>
        /// <param name="values">An ordered dictionary of shells.</param>
        public ShellConfiguration(Dictionary<char, int> values)
        {
            for (var i = 0; i < values.Count; i++)
            {
                int capacity = ChemistryUtils.GetShellCapacity(i);
                KeyValuePair<char, int> current = values.ToList()[i];

                if (current.Value > capacity || current.Value < 0)
                    throw new IndexOutOfRangeException(nameof(current.Value));

                Add(new Shell(current.Key, current.Value));
            }
        }

        /// <summary>
        /// Gets a <see cref="Shell"/> with the specified symbol.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <returns></returns>
        public Shell this[char symbol] => this.FirstOrDefault(x => x.Symbol == symbol);

        /// <summary>
        /// Gets the amount of paired electrons in the <see cref="ShellConfiguration"/>.
        /// </summary>
        public int PairedElectrons => this.Sum(x => x.PairedElectrons);

        /// <summary>
        /// Gets the amount of unpaired electrons in the <see cref="ShellConfiguration"/>.
        /// </summary>
        public int UnpairedElectrons => this.Sum(x => x.UnpairedElectrons);

        /// <summary>
        /// Returns the valence shell of the <see cref="ShellConfiguration"/>.
        /// </summary>
        /// <returns></returns>
        public Shell GetValenceShell()
        {
            return this.Last();
        }

        /// <summary>
        /// Converts the current <see cref="ShellConfiguration"/> instance to a dictionary.
        /// </summary>
        public Dictionary<char, int> ToDictionary()
        {
            return this.ToDictionary(x => x.Symbol, y => y.Electrons);
        }
    }
}
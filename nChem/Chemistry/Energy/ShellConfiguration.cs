using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace nChem.Chemistry.Energy
{
    /// <summary>
    /// Represents a configuration for the electrons within an <see cref="Atom"/>.
    /// </summary>
    public sealed class ShellConfiguration
    {
        /// <summary>
        /// Initializes an instance of the <see cref="ShellConfiguration"/> class.
        /// </summary>
        /// <param name="collection">The collection of shells.</param>
        public ShellConfiguration(ICollection<Shell> collection)
        {
            Shells = collection;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="ShellConfiguration"/> class.
        /// </summary>
        /// <param name="values">An ordered collection of shells.</param>
        public ShellConfiguration(int[] values)
        {
            Shells = new Collection<Shell>();
            for (var i = 0; i < values.Length; i++)
            {
                int value = values[i];

                if (value > ChemistryUtils.GetShellCapacity(i) || value < 0)
                    throw new IndexOutOfRangeException(nameof(value));

                Shells.Add(new Shell(ChemistryUtils.ShellLabels[i], value));
            }
        }

        /// <summary>
        /// Initializes an instance of the <see cref="ShellConfiguration"/> class.
        /// </summary>
        /// <param name="values">An ordered dictionary of shells.</param>
        public ShellConfiguration(Dictionary<char, int> values)
        {
            Shells = new Collection<Shell>();
            for (var i = 0; i < values.Count; i++)
            {
                int capacity = ChemistryUtils.GetShellCapacity(i);
                KeyValuePair<char, int> current = values.ToList()[i];

                if (current.Value > capacity || current.Value < 0)
                    throw new IndexOutOfRangeException(nameof(current.Value));

                Shells.Add(new Shell(current.Key, current.Value));
            }
        }

        /// <summary>
        /// Gets a <see cref="Shell"/> with the specified symbol.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <returns></returns>
        public Shell this[char symbol] => Shells.FirstOrDefault(x => x.Symbol == symbol);

        /// <summary>
        /// Gets the shells of the <see cref="ShellConfiguration"/>.
        /// </summary>
        public ICollection<Shell> Shells { get; }

        /// <summary>
        /// Gets the amount of paired electrons in the <see cref="ShellConfiguration"/>.
        /// </summary>
        public int PairedElectrons => Shells.Sum(x => x.PairedElectrons);

        /// <summary>
        /// Gets the amount of unpaired electrons in the <see cref="ShellConfiguration"/>.
        /// </summary>
        public int UnpairedElectrons => Shells.Sum(x => x.UnpairedElectrons);

        /// <summary>
        /// Returns all the electrons in the <see cref="ShellConfiguration"/>.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Electron> GetElectrons()
        {
            return Shells
                .SelectMany(x => x.Subshells
                .SelectMany(y => y.Orbitals
                    .SelectMany(k => k.GetElectrons())))
                        .Where(x => x != null);
        }

        /// <summary>
        /// Returns the valence shell of the <see cref="ShellConfiguration"/>.
        /// </summary>
        /// <returns></returns>
        public Shell GetValenceShell()
        {
            return Shells.Last();
        }

        /// <summary>
        /// Converts the current <see cref="ShellConfiguration"/> instance to a dictionary.
        /// </summary>
        public Dictionary<char, int> ToDictionary()
        {
            return Shells.ToDictionary(x => x.Symbol, y => y.Electrons);
        }
    }
}
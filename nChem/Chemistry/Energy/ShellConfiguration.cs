using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using nChem.Chemistry.Particles;

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
        /// <param name="element">The parent element.</param>
        /// <param name="collection">The collection of shells.</param>
        public ShellConfiguration(Element element, ICollection<Shell> collection)
        {
            Shells = collection;
            Element = element;

            Populate();
        }

        /// <summary>
        /// Initializes an instance of the <see cref="ShellConfiguration"/> class.
        /// </summary>
        /// <param name="element">The parent element.</param>
        /// <param name="values">An ordered collection of shells.</param>
        public ShellConfiguration(Element element, int[] values)
        {
            Shells = new Collection<Shell>();
            Element = element;

            for (var i = 0; i < values.Length; i++)
            {
                int value = values[i];

                if (value > ChemistryUtils.GetShellCapacity(i) || value < 0)
                    throw new IndexOutOfRangeException(nameof(value));

                Shells.Add(new Shell(ChemistryUtils.ShellLabels[i], value));
            }

            Populate();
        }

        /// <summary>
        /// Initializes an instance of the <see cref="ShellConfiguration"/> class.
        /// </summary>
        /// <param name="element">The parent element.</param>
        /// <param name="values">An ordered dictionary of shells.</param>
        public ShellConfiguration(Element element, Dictionary<char, int> values)
        {
            Shells = new Collection<Shell>();
            Element = element;

            for (var i = 0; i < values.Count; i++)
            {
                int capacity = ChemistryUtils.GetShellCapacity(i);
                KeyValuePair<char, int> current = values.ToList()[i];

                if (current.Value > capacity || current.Value < 0)
                    throw new IndexOutOfRangeException(nameof(current.Value));

                Shells.Add(new Shell(current.Key, current.Value));
            }

            Populate();
        }

        /// <summary>
        /// Gets the parent element of the <see cref="ShellConfiguration"/>.
        /// </summary>
        public Element Element { get; }

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
        /// Populates the current <see cref="ShellConfiguration"/> instance.
        /// </summary>
        public void Populate()
        {
            int left = Element.Electrons;
            for (var i = 0; i < ChemistryUtils.EnergyLevelConfiguration.Count; i++)
            {
                if (left == 0)
                    break;

                KeyValuePair<char, char[]> pair = ChemistryUtils.EnergyLevelConfiguration.ToList()[i];
                Shell shell = this[pair.Key];

                for (int x = 0; x < pair.Value.Length; x++)
                {
                    if (left == 0)
                        break;

                    Subshell subshell = shell[pair.Value[x]];

                    if (subshell.Electrons > 0)
                        continue;

                    if (left - subshell.Capacity < 0)
                    {
                        subshell.Populate(left);
                        left = 0;
                    }
                    else
                    {
                        subshell.Populate(subshell.Capacity);
                        left -= subshell.Capacity;
                    }

                    int stepsDown = x;
                    for (int z = 0; z < stepsDown; z++)
                    {
                        if (left == 0)
                            break;

                        Subshell diagonal = Shells.ToList()[i + z + 1].Subshells[x - (z + 1)];
                        if (left - diagonal.Capacity < 0)
                        {
                            diagonal.Populate(left);
                            left = 0;
                        }
                        else
                        {
                            diagonal.Populate(diagonal.Capacity);
                            left -= diagonal.Capacity;
                        }
                    }
                }
            }

            foreach (var shell in Shells.ToArray())
            {
                foreach (var subshell in shell.Subshells.ToArray())
                {
                    if (subshell.Electrons == 0)
                        shell.Subshells.Remove(subshell);
                }

                if (shell.Electrons == 0)
                    Shells.Remove(shell);
            }
        }

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

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var i = 0; i < Shells.Count; i++)
            {
                Shell current = Shells.ToArray()[i];

                if (i < Shells.Count - 1)
                {
                    sb.Append(current + " ");
                    continue;
                }

                sb.Append(current);
            }

            return sb.ToString();
        }
    }
}
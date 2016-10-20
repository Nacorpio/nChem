using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace nChem
{
    /// <summary>
    /// Represents an electron shell of which electrons are stored within.
    /// </summary>
    public sealed class Shell
    {
        /// <summary>
        /// Initializes an instance of the <see cref="Shell"/> class.
        /// </summary>
        /// <param name="symbol">The unique symbol for the shell.</param>
        /// <param name="electrons">The initial amount of electrons.</param>
        public Shell(char symbol, int electrons)
        {
            Symbol = symbol;
            EnergyLevel = Index + 1;

            Subshell s = new Subshell(Symbol, 's', 2),
                     p = new Subshell(Symbol, 'p', 6),
                     d = new Subshell(Symbol, 'd', 10),
                     f = new Subshell(Symbol, 'f', 14);

            Subshells = new List<Subshell> { s };

            if (Index >= 1 && electrons > s.Capacity)
                Subshells.Add(p);

            if (Index >= 2 && electrons > p.Capacity)
                Subshells.Add(d);

            if (Index >= 3 && electrons > d.Capacity)
                Subshells.Add(f);

            Populate(electrons);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="Shell"/> class.
        /// </summary>
        /// <param name="symbol">The unique symbol for the shell.</param>
        public Shell(char symbol)
            : this(symbol, 0)
        { }

        /// <summary>
        /// Gets the unique symbol of the <see cref="Shell"/>.
        /// </summary>
        [JsonProperty("symbol")]
        public char Symbol { get; }

        /// <summary>
        /// Gets the zero-based index of the <see cref="Shell"/>.
        /// </summary>
        public int Index => ChemistryUtils.ShellLabels.ToList().FindIndex(x => x == Symbol);

        /// <summary>
        /// Gets the maximum amount of electrons that can be stored within the <see cref="Shell"/>.
        /// </summary>
        [JsonProperty("symbol")]
        public int Capacity => Subshells.Sum(x => x.Capacity);

        /// <summary>
        /// Gets the energy level of the <see cref="Shell"/>.
        /// </summary>
        [JsonProperty("energyLevel")]
        public int EnergyLevel { get; }

        /// <summary>
        /// Gets or sets the amount of electrons within the <see cref="Shell"/>.
        /// </summary>
        [JsonProperty("electrons")]
        public int Electrons => Subshells.Sum(x => x.Electrons);

        /// <summary>
        /// Gets the amount of paired electrons within the <see cref="Shell"/>.
        /// </summary>
        [JsonProperty("pairedElectrons")]
        public int PairedElectrons => Subshells.Sum(x => x.PairedElectrons);

        /// <summary>
        /// Gets the amount of unpaired electrons within the <see cref="Shell"/>.
        /// </summary>
        [JsonProperty("unpairedElectrons")]
        public int UnpairedElectrons => Subshells.Sum(x => x.UnpairedElectrons);

        /// <summary>
        /// Gets the subshells of the <see cref="Shell"/>.
        /// </summary>
        public List<Subshell> Subshells { get; }

        /// <summary>
        /// Gets a subshell with the specified label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns></returns>
        public Subshell this[char label] => Subshells.FirstOrDefault(x => x.Label == label);

        /// <summary>
        /// Adds the left-hand operand with the right-hand operand.
        /// </summary>
        /// <param name="s1">The left-hand operand.</param>
        /// <param name="i2">The right-hand operand.</param>
        /// <returns></returns>
        public static Shell operator +(Shell s1, int i2)
        {
            if (s1.Electrons + i2 > s1.Capacity)
                throw new IndexOutOfRangeException();

            return new Shell(s1.Symbol, s1.Electrons + i2);
        }

        /// <summary>
        /// Subtracts the left-hand operand with the right-hand operand.
        /// </summary>
        /// <param name="s1">The left-hand operand.</param>
        /// <param name="i2">The right-hand operand.</param>
        /// <returns></returns>
        public static Shell operator -(Shell s1, int i2)
        {
            if (s1.Electrons - i2 < 0)
                throw new IndexOutOfRangeException();

            return new Shell(s1.Symbol, s1.Electrons - i2);
        }

        /// <summary>
        /// Populates the <see cref="Shell"/> with <c>n</c> electrons.
        /// </summary>
        /// <param name="n">The amount of electrons to populate with.</param>
        public void Populate(int n)
        {
            int remainingElectrons = n;

            int i = 0;
            while (remainingElectrons > 0)
            {
                Subshell subshell = Subshells[i];
                if (remainingElectrons > subshell.Capacity)
                {
                    subshell.Populate(subshell.Capacity);
                    remainingElectrons -= subshell.Capacity;

                    i++;
                    continue;
                }

                subshell.Populate(remainingElectrons);
                remainingElectrons = 0;

                i++;
            }
        }

        /// <summary>
        /// Determines whether the <see cref="Shell"/> has reached its maximum capacity of electrons that can be stored.
        /// </summary>
        /// <returns></returns>
        public bool IsFull()
        {
            return Electrons == Capacity;
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Symbol + ": ");

            foreach (var subshell in Subshells)
                sb.Append($"{subshell}{subshell.Electrons} ");

            return sb.ToString().Trim();
        }
    }
}
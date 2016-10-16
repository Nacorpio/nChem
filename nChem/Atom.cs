using System.Collections.Generic;
using System.Text;

namespace nChem
{
    /// <summary>
    /// Represents an atom of an <see cref="Element"/>.
    /// </summary>
    public sealed class Atom
    {
        /// <summary>
        /// Initializes an instance of the <see cref="Atom"/> class.
        /// </summary>
        /// <param name="element">The element of the atom.</param>
        public Atom(Element element)
        {
            Element = element;
            Electrons = Element.Electrons;
            Neutrons = Element.Neutrons;
            Protons = Element.Protons;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="Atom"/> class.
        /// </summary>
        /// <param name="atomicNumber">The atomic number of the element.</param>
        public Atom(int atomicNumber)
            : this(new Element(atomicNumber))
        { }

        /// <summary>
        /// Gets or sets the amount of electrons within the <see cref="Atom"/>.
        /// </summary>
        public int Electrons { get; set; }

        /// <summary>
        /// Gets or sets the amount of neutrons within the <see cref="Atom"/>.
        /// </summary>
        public int Neutrons { get; set; }

        /// <summary>
        /// Gets or sets the amount of neutrons within the <see cref="Atom"/>.
        /// </summary>
        public int Protons { get; set; }

        /// <summary>
        /// Gets the element of the <see cref="Atom"/>.
        /// </summary>
        public Element Element { get; }
        
        /// <summary>
        /// Returns the shells of the <see cref="Element"/>.
        /// </summary>
        /// <returns></returns>
        public ShellConfiguration GetShellConfiguration()
        {
            var shells = new Dictionary<char, int>();
            int shellCount = MathUtilities.GetShellCount(Element.Electrons);

            int electrons = Element.Electrons;

            for (var i = 0; i < shellCount; i++)
            {
                if (electrons == 0)
                    break;

                int capacity = MathUtilities.GetShellCapacity(i);
                char shell = MathUtilities.ShellLabels[i];

                if (!shells.ContainsKey(shell))
                    shells.Add(shell, 0);

                if (electrons > capacity)
                {
                    shells[shell] = capacity;
                    electrons -= capacity;

                    continue;
                }

                shells[shell] = electrons;
                electrons = 0;
            }

            return new ShellConfiguration(shells);
        }

        /// <summary>
        /// Determines whether the current <see cref="Atom"/> is an alkali metal.
        /// </summary>
        /// <returns></returns>
        public bool IsAlkaliMetal()
        {
            return GetShellConfiguration().GetValenceShell().Electrons == 1;
        }

        /// <summary>
        /// Determines whether the current <see cref="Atom"/> is a halogen.
        /// </summary>
        /// <returns></returns>
        public bool IsHalogen()
        {
            return GetShellConfiguration().GetValenceShell().Electrons ==
                   GetShellConfiguration().GetValenceShell().Capacity - 1;
        }

        /// <summary>
        /// Determines whether the <see cref="Atom"/> is an ion.
        /// </summary>
        /// <returns></returns>
        public bool IsIon()
        {
            return Electrons != Protons;
        }

        /// <summary>
        /// Determines whether the <see cref="Atom"/> has a positive charge.
        /// </summary>
        /// <returns></returns>
        public bool HasPositiveCharge()
        {
            return Electrons < Protons;
        }

        /// <summary>
        /// Determines whether the <see cref="Atom"/> has a negative charge.
        /// </summary>
        /// <returns></returns>
        public bool HasNegativeCharge()
        {
            return Electrons > Protons;
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Element.Symbol);

            if (IsIon())
                sb.Append(HasPositiveCharge() ? "⁺" : "⁻");

            return sb.ToString();
        }
    }
}
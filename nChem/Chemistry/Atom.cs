using System;
using System.Collections.Generic;
using nChem.Chemistry.Energy;

namespace nChem.Chemistry
{
    /// <summary>
    /// Represents an atom of an <see cref="Element"/>.
    /// </summary>
    public sealed class Atom : IAtomic
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
        /// Initializes an instance of teh <see cref="Atom"/> class.
        /// </summary>
        /// <param name="element">The element of the atom.</param>
        /// <param name="charge">The charge of the atom.</param>
        public Atom(Element element, int charge = 0)
            : this(element)
        {
            if (Electrons + (-charge) < 0)
                throw new ArgumentOutOfRangeException(nameof(charge), "The amount of electrons inside an atom can't be less than zero.");

            Electrons += -charge;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="Atom"/> class.
        /// </summary>
        /// <param name="atomicNumber">The atomic number of the element.</param>
        public Atom(int atomicNumber)
            : this(Element.Create(atomicNumber))
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
        /// Gets the magnetism of the <see cref="Atom"/>.
        /// </summary>
        public Magnetism Magnetism
            => GetShellConfiguration().UnpairedElectrons > 0 ? Magnetism.Paramagnetic : Magnetism.Diamagnetic;

        /// <summary>
        /// Returns the shells of the <see cref="Element"/>.
        /// </summary>
        /// <returns></returns>
        public ShellConfiguration GetShellConfiguration()
        {
            //var shells = new Dictionary<char, int>();
            //int shellCount = ChemistryUtils.GetShellCount(Element.Electrons);

            //int electrons = Element.Electrons;

            //for (var i = 0; i < shellCount; i++)
            //{
            //    if (electrons == 0)
            //        break;

            //    int capacity = ChemistryUtils.GetShellCapacity(i);
            //    char shell = ChemistryUtils.ShellLabels[i];

            //    if (!shells.ContainsKey(shell))
            //        shells.Add(shell, 0);

            //    if (electrons > capacity)
            //    {
            //        shells[shell] = capacity;
            //        electrons -= capacity;

            //        continue;
            //    }

            //    shells[shell] = electrons;
            //    electrons = 0;
            //}

            return new ShellConfiguration(Element, new Dictionary<char, int>
            {
                {'k', 2},
                {'l', 8},
                {'m', 18},
                {'n', 32},
                {'o', 50},
                {'p', 72},
                {'q', 96}
            });
        }

        /// <summary>
        /// Converts the current <see cref="Atom"/> instance to an ion.
        /// </summary>
        /// <returns></returns>
        public Ion ToIon()
        {
            if (!IsIon())
                throw new Exception("Can't convert an uncharged atom to an ion.");

            return new Ion(this);
        }

        /// <summary>
        /// Determines whether the <see cref="IAtomic"/> implementation is a compound.
        /// </summary>
        /// <returns></returns>
        public bool IsCompound()
        {
            return false;
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
        /// Returns the atomic weight of the <see cref="Atom"/>.
        /// </summary>
        /// <returns></returns>
        public float? GetAtomicWeight()
        {
            return Element.AtomicWeight;
        }

        /// <summary>
        /// Returns the elements in the <see cref="IAtomic"/> implementation.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Element> GetElements()
        {
            return new[] {Element};
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            var other = obj as Atom;

            if (other == null)
                return false;

            return Element.AtomicNumber == other.Element.AtomicNumber
                    && Electrons == other.Electrons
                    && Protons == other.Protons
                    && Neutrons == other.Neutrons;
        }

        /// <summary>Serves as the default hash function. </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Electrons;
                hashCode = (hashCode*397) ^ Neutrons;
                hashCode = (hashCode*397) ^ Protons;
                hashCode = (hashCode*397) ^ (Element?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Element.Symbol;
        }
    }
}
using System;

namespace nChem
{
    /// <summary>
    /// Represents an <see cref="Atom"/> with a positive or negative charge.
    /// </summary>
    public sealed class Ion
    {
        /// <summary>
        /// Initializes an instance of the <see cref="Ion"/> class.
        /// </summary>
        /// <param name="atom">The base atom.</param>
        public Ion(Atom atom)
        {
            if (atom == null)
                throw new ArgumentNullException(nameof(atom));

            if (Atom.Electrons == Atom.Protons)
                throw new ArgumentException("The specified atom is not an ion.", nameof(atom));

            Atom = atom;
        }

        /// <summary>
        /// Gets the underlying atom of the <see cref="Ion"/>.
        /// </summary>
        public Atom Atom { get; }

        /// <summary>
        /// Determines whether the <see cref="Atom"/> has a positive charge.
        /// </summary>
        /// <returns></returns>
        public bool HasPositiveCharge()
        {
            return Atom.Electrons < Atom.Protons;
        }

        /// <summary>
        /// Determines whether the <see cref="Atom"/> has a negative charge.
        /// </summary>
        /// <returns></returns>
        public bool HasNegativeCharge()
        {
            return Atom.Electrons > Atom.Protons;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nChem.Chemistry
{
    /// <summary>
    /// Represents a compound of atoms.
    /// </summary>
    public class Compound : IAtomic
    {
        /// <summary>
        /// Initializes an instance of the <see cref="Compound"/> class.
        /// </summary>
        /// <param name="enumerable">An enumerable.</param>
        public Compound(IEnumerable<Stack> enumerable)
        {
            Stacks = enumerable;
        }

        /// <summary>
        /// Gets the stacks of the <see cref="Compound"/>.
        /// </summary>
        public IEnumerable<Stack> Stacks { get; }

        /// <summary>
        /// Gets the amount of electrons in the <see cref="Compound"/>.
        /// </summary>
        public int Electrons => Stacks.Sum(x => x.Size * x.Atom.Electrons);

        /// <summary>
        /// Gets the amount of protons in the <see cref="Compound"/>.
        /// </summary>
        public int Protons => Stacks.Sum(x => x.Size * x.Atom.Protons);

        /// <summary>
        /// Gets the amount of neutrons in the <see cref="Compound"/>.
        /// </summary>
        public int Neutrons => Stacks.Sum(x => x.Size * x.Atom.Neutrons);

        /// <summary>
        /// Returns the total atomic weight of the <see cref="Compound"/>.
        /// </summary>
        /// <returns></returns>
        public float GetAtomicWeight()
        {
            return Stacks
                .Sum(x => x.GetAtomicWeight());
        }
        
        /// <summary>
        /// Returns an array of all elements that the <see cref="Compound"/> contains.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Element> GetElements()
        {
            return Stacks
                .SelectMany(x => x.Atom.GetElements())
                .Distinct();
        }

        /// <summary>
        /// Returns the oxidation number of the <see cref="IAtomic"/> implementation.
        /// </summary>
        /// <returns></returns>
        public int[] GetOxidationNumbers()
        {
            int target = IsIon() ? new Ion(Stacks.ToArray()).GetCharge() : 0;
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether the <see cref="Compound"/> is an ion.
        /// </summary>
        /// <returns></returns>
        public bool IsIon()
        {
            return Protons != Electrons;
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var stack in Stacks)
                sb.Append(stack);

            return sb.ToString();
        }
    }
}
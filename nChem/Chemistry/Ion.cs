using System;
using System.Collections.Generic;
using System.Linq;

namespace nChem.Chemistry
{
    /// <summary>
    /// Represents an <see cref="Atom"/> with a positive or negative charge.
    /// </summary>
    public sealed class Ion : IChargeable
    {
        /// <summary>
        /// Initializes an instance of the <see cref="Ion"/> class.
        /// </summary>
        /// <param name="enumerable">The stacks.</param>
        public Ion(IEnumerable<Stack> enumerable)
        {
            Stacks = enumerable;
        }

        /// <summary>
        /// Gets the stacks of the 
        /// </summary>
        public IEnumerable<Stack> Stacks { get; }

        /// <summary>
        /// Determines whether the <see cref="Ion"/> is polyatomic (more than one atom).
        /// </summary>
        /// <returns></returns>
        public bool IsPolyatomic()
        {
            return Stacks.Sum(x => x.Size) > 1;
        }

        /// <summary>
        /// Determines whether the <see cref="Ion"/> is a cation (has a positive charge).
        /// </summary>
        /// <returns></returns>
        public bool IsCation()
        {
            return GetCharge() > 0;
        }

        /// <summary>
        /// Determines whether the <see cref="Ion"/> is an anion (has a negative charge).
        /// </summary>
        /// <returns></returns>
        public bool IsAnion()
        {
            return GetCharge() < 0;
        }

        /// <summary>
        /// Determines whether the <see cref="Ion"/> is monatomic (only one element).
        /// </summary>
        /// <returns></returns>
        public bool IsMonatomic()
        {
            return Stacks.Count() == 1;
        }

        /// <summary>
        /// Returns the charge of the <see cref="IChargeable"/> implementation.
        /// </summary>
        /// <returns></returns>
        public int GetCharge()
        {
            return Stacks.Sum(x => x.Atom.Protons * x.Size) - Stacks.Sum(x => x.Atom.Electrons * x.Size);
        }

        /// <summary>
        /// Determines whether the <see cref="Ion"/> contains a specific element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        internal bool Contains(Element element)
        {
            return Stacks.Any(x => x.Atom.GetElements().Any(y => y.Equals(element)));
        }

        /// <summary>
        /// Returns the oxidation number of the <see cref="IChargeable"/> implementation.
        /// </summary>
        /// <returns></returns>
        public int GetOxidationNumber()
        {
            if (!IsMonatomic())
                throw new NotImplementedException();

            var element = Stacks
                .First().Atom.GetElements()
                .First();

            switch (element.Group)
            {
                case 1:
                    return 1;

                case 2:
                    return 2;

                case 17:
                    return -1;

                default:
                {
                    switch (element.AtomicNumber)
                    {
                        case 1:
                            return 1;

                        case 8:
                            return -2;

                        case 9:
                            return -1;
                    }

                    throw new Exception("The oxidation number couldn't be calculated.");
                }
            }
        }

        /// <summary>
        /// Determines whether the <see cref="IChargeable"/> implementation is charged.
        /// </summary>
        /// <returns></returns>
        public bool IsCharged()
        {
            return true;
        }
    }
}
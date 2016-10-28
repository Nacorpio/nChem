using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nChem.Chemistry
{
    /// <summary>
    /// Represents a compound of atoms.
    /// </summary>
    public sealed class Compound : IAtomic
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
        /// Converts a specific array of <see cref="Element"/> to a compound.
        /// </summary>
        /// <param name="elements">The elements to convert.</param>
        public static implicit operator Compound(Element[] elements)
        {
            return new Compound(elements.Select(x => new Stack(x)));
        }

        /// <summary>
        /// Converts a specific array of <see cref="Atom"/> to a compound.
        /// </summary>
        /// <param name="atoms">The atoms to convert.</param>
        public static implicit operator Compound(Atom[] atoms)
        {
            return new Compound(atoms.Select(x => new Stack(x)));
        }

        /// <summary>
        /// Converts a specific array of <see cref="Stack"/> to a compound.
        /// </summary>
        /// <param name="stacks">The stacks to convert.</param>
        public static implicit operator Compound(Stack[] stacks)
        {
            return new Compound(stacks);
        }

        /// <summary>
        /// Returns the total atomic weight of the <see cref="Compound"/>.
        /// </summary>
        /// <returns></returns>
        public float? GetAtomicWeight()
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
        public bool TryGetOxidationNumbers(out int[] numbers)
        {
            int target = IsIon() ? new Ion(Stacks.ToArray()).GetCharge() : 0;

            var results = new List<int>();
            var unknown = new List<int>();

            int i = 0;
            foreach (var stack in Stacks)
            {
                i++;

                var compound = stack.Atom as Compound;
                if (compound != null)
                {
                    int[] cn;
                    if (compound.TryGetOxidationNumbers(out cn))
                        results.AddRange(cn);

                    continue;
                }

                var atom = stack.Atom as Atom;
                if (atom == null)
                    continue;

                switch (atom.Element.Group)
                {
                    case 1:
                        results.Add(1 * stack.Size);
                        continue;

                    case 2:
                        results.Add(2 * stack.Size);
                        continue;

                    case 17:
                        {
                            if (GetElements().Any(x => x.AtomicNumber == 8))
                            {
                                unknown.Add(i - 1);
                                continue;
                            }

                            results.Add(-1 * stack.Size);
                            continue;
                        }
                }

                switch (atom.Element.AtomicNumber)
                {
                    case 1:
                        results.Add(GetElements().Any(x => x.IsMetal) ? -1 : 1 * stack.Size);
                        continue;

                    case 8:
                        results.Add(-2 * stack.Size);
                        continue;

                    case 9:
                        results.Add(-1 * stack.Size);
                        continue;
                }

                unknown.Add(i - 1);
            }

            if (unknown.Count > 0)
            {
                foreach (var index in unknown.ToArray())
                {
                    int x = byte.MaxValue;

                    while (x + results.Sum(y => y) != target)
                        x--;

                    results.Insert(index, x);
                }
            }

            numbers = results.ToArray();
            return numbers.Sum(x => x) == target;
        }

        /// <summary>
        /// Determines whether the <see cref="Compound"/> is an ion.
        /// </summary>
        /// <returns></returns>
        public bool IsIon()
        {
            return Protons != Electrons;
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            var other = obj as Compound;

            if (other == null)
                return false;

            return other.Stacks.SequenceEqual(Stacks);
        }

        /// <summary>Serves as the default hash function. </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Stacks?.GetHashCode() ?? 0;
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
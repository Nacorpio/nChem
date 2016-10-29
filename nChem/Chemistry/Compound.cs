using System;
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
            var stacks = enumerable.ToList();

            Stacks = stacks;
            Electrons = Stacks.Sum(x => x.Size * x.Atom.Electrons);
            Neutrons = Stacks.Sum(x => x.Size * x.Atom.Neutrons);
            Protons = Stacks.Sum(x => x.Size * x.Atom.Protons);

            foreach (var element in GetElements().ToArray())
            {
                int count = stacks.Count(x => Equals(x.Atom.GetElements().ToArray()[0], element));

                if (count <= 1) continue;

                int size = stacks.Where(x => Equals(x.Atom.GetElements().ToArray()[0], element)).Sum(x => x.Size);
                var stack = new Stack(element, size);

                int index = stacks.FindIndex(x => x.Atom.GetElements().ToArray()[0].Equals(element));

                stacks.RemoveAll(x => x.Atom.GetElements().ToArray()[0].Equals(element));
                stacks.Insert(index, stack);
            }
        }

        /// <summary>
        /// Gets the stacks of the <see cref="Compound"/>.
        /// </summary>
        public IEnumerable<Stack> Stacks { get; }

        /// <summary>
        /// Gets the amount of electrons in the <see cref="Compound"/>.
        /// </summary>
        public int Electrons { get; set; }

        /// ;<summary>
        /// Gets the amount of protons in the <see cref="Compound"/>.
        /// </summary>
        public int Protons { get; }

        /// <summary>
        /// Gets the amount of neutrons in the <see cref="Compound"/>.
        /// </summary>
        public int Neutrons { get; }

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
        public bool TryGetOxidationNumbers(out Dictionary<Element, int> numbers)
        {
            int target = IsIon() ? new Ion(this).Charge : 0;

            var results = new Dictionary<Element, int>();
            Tuple<int, Element> unknown = null;

            int i = 0;
            foreach (var stack in Stacks)
            {
                i++;

                var compound = stack.Atom as Compound;
                if (compound != null)
                {
                    Dictionary<Element, int> cn;

                    if (compound.TryGetOxidationNumbers(out cn))
                        cn.ToList().ForEach(x => results.Add(x.Key, x.Value));

                    continue;
                }

                var atom = stack.Atom as Atom;
                if (atom == null)
                    continue;

                switch (atom.Element.Group)
                {
                    case 1:
                        results.Add(atom.Element, 1 * stack.Size);
                        continue;

                    case 2:
                        results.Add(atom.Element, 2 * stack.Size);
                        continue;

                    case 17:
                        {
                            if (GetElements().Any(x => x.AtomicNumber == 8))
                            {
                                unknown = new Tuple<int, Element>(i - 1, atom.Element);
                                continue;
                            }

                            results.Add(atom.Element, -1 * stack.Size);
                            continue;
                        }
                }

                switch (atom.Element.AtomicNumber)
                {
                    case 1:
                        results.Add(atom.Element, GetElements().Any(x => x.IsMetal) ? -1 : 1 * stack.Size);
                        continue;

                    case 8:
                        if (results.ContainsKey(atom.Element))
                        {
                            results[atom.Element] += -2*stack.Size;
                            continue;
                        }

                        results.Add(atom.Element, -2 * stack.Size);
                        continue;

                    case 9:
                        results.Add(atom.Element, -1 * stack.Size);
                        continue;
                }

                unknown = new Tuple<int, Element>(i - 1, atom.Element);
            }

            if (unknown != null)
            {
                int x = byte.MaxValue;

                while (x + results.Sum(y => y.Value) != target)
                    x--;

                results[unknown.Item2] = x;
            }

            numbers = results;
            return numbers.Sum(x => x.Value) == target;
        }

        /// <summary>
        /// Determines whether the <see cref="Compound"/> is an ion.
        /// </summary>
        /// <returns></returns>
        public bool IsIon()
        {
            return Protons != Electrons;
        }

        /// <summary>
        /// Determines whether the <see cref="IAtomic"/> implementation is a compound.
        /// </summary>
        /// <returns></returns>
        public bool IsCompound()
        {
            return true;
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
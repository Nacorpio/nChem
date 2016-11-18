using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nChem.Chemistry
{
    /// <summary>
    /// Represents a compound of atoms.
    /// </summary>
    public sealed class Compound : List<Stack>, IAtomic
    {
        /// <summary>
        /// Initializes an instance of the <see cref="Compound"/> class.
        /// </summary>
        public Compound()
        { }

        /// <summary>
        /// Initializes an instance of the <see cref="Compound"/> class.
        /// </summary>
        /// <param name="enumerable">An enumerable.</param>
        /// <param name="charge">The charge.</param>
        public Compound(IEnumerable<Stack> enumerable, int charge = 0)
            : base(enumerable)
        {
            Electrons = this.Sum(x => x.Size * x.Atom.Electrons) + -charge;
            Neutrons = this.Sum(x => x.Size * x.Atom.Neutrons);
            Protons = this.Sum(x => x.Size * x.Atom.Protons);

            Organize();
        }

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
        /// Returns the total atomic weight of the <see cref="Compound"/>.
        /// </summary>
        /// <returns></returns>
        public float? AtomicWeight => this.Sum(x => x.AtomicWeight);

        /// <summary>
        /// Organizes the <see cref="Compound"/>.
        /// </summary>
        internal void Organize()
        {
            foreach (var element in GetElements().ToArray())
            {
                int count = this.Count(x => Equals(x.Atom.GetElements().ToArray()[0], element));

                if (count <= 1) continue;

                int size = this.Where(x => Equals(x.Atom.GetElements().ToArray()[0], element)).Sum(x => x.Size);
                var stack = new Stack(element, size);

                int index = FindIndex(x => x.Atom.GetElements().ToArray()[0].Equals(element));

                RemoveAll(x => x.Atom.GetElements().ToArray()[0].Equals(element));
                Insert(index, stack);
            }
        }

        public Compound With(Element element, int size = 1)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            Add(new Stack(element, size));
            return this;
        }

        public Compound With(params IAtomic[] items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            AddRange(items.Select(x => new Stack(x)));
            return this;
        }

        public Compound With(params Element[] elements)
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            AddRange(elements.Select(x => new Stack(x)));
            return this;
        }

        public Compound With(params Stack[] stacks)
        {
            if (stacks == null)
                throw new ArgumentNullException(nameof(stacks));

            AddRange(stacks);
            return this;
        }

        /// <summary>
        /// Determines whether the <see cref="Compound"/> contains a stack with the specific element and minimum size.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="size">The minimum size.</param>
        /// <returns></returns>
        public bool Contains(Element element, int size)
        {
            return this.Any(x => x.Atom.GetElements().Count() == 1 && x.Atom.GetElements().First().Equals(element) && x.Size >= size);
        }

        /// <summary>
        /// Determines whether the <see cref="Compound"/> contains a specific element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public bool Contains(Element element)
        {
            return GetElements().Any(x => x.Equals(element));
        }

        /// <summary>
        /// Returns an array of all elements that the <see cref="Compound"/> contains.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Element> GetElements()
        {
            return this
                .SelectMany(x => x.Atom.GetElements())
                .Distinct();
        }

        /// <summary>
        /// Attempts to calculate the oxidation number of the <see cref="Compound"/>.
        /// </summary>
        /// <returns>True if successful; otherwise false.</returns>
        public bool TryGetOxidationNumbers(out Dictionary<Stack, int> numbers)
        {
            int target = IsIon() ? ToIon().GetCharge() : 0;
            var results = new Dictionary<Stack, int>();
            Tuple<int, Element, Stack> unknown = null;
            
            int i = 0;
            foreach (var stack in this)
            {
                i++;

                var compound = stack.Atom as Compound;
                if (compound != null)
                {
                    Dictionary<Stack, int> cn;

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
                        results.Add(stack, 1 * stack.Size);
                        continue;

                    case 2:
                        results.Add(stack, 2 * stack.Size);
                        continue;

                    case 17:
                        {
                            if (GetElements().Any(x => x.AtomicNumber == 8))
                            {
                                unknown = new Tuple<int, Element, Stack>(i - 1, atom.Element, stack);
                                continue;
                            }

                            results.Add(stack, -1 * stack.Size);
                            continue;
                        }
                }

                switch (atom.Element.AtomicNumber)
                {
                    // Hydrogen
                    case 1:
                        results.Add(stack, GetElements().Any(x => x.IsMetal()) ? -1 : 1 * stack.Size);
                        continue;

                    // Oxygen
                    case 8:
                        if (results.ContainsKey(atom.Element))
                        {
                            results[atom.Element] += -2 * stack.Size;
                            continue;
                        }

                        results.Add(stack, -2 * stack.Size);
                        continue;

                    // Fluor
                    case 9:
                        results.Add(stack, -1 * stack.Size);
                        continue;
                }

                unknown = new Tuple<int, Element, Stack>(i - 1, atom.Element, stack);
            }

            if (unknown != null)
            {
                int x = byte.MaxValue;
                int sum = results.Sum(y => y.Value);    

                while (x + sum != target)
                    x--;

                results[new Stack(unknown.Item2, unknown.Item3.Size)] = x;
            }

            numbers = results.ToDictionary(x => x.Key, y => y.Value / y.Key.Size);
            return numbers.Sum(x => x.Value * x.Key.Size) == target;
        }

        #region MyRegion
        
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
        /// Converts the current <see cref="IAtomic"/> implement to an <see cref="Ion"/> instance.
        /// </summary>
        /// <returns></returns>
        public Ion ToIon()
        {
            if (!IsIon())
                throw new Exception("Can't convert an uncharged atom to an ion.");

            return new Ion(this);
        }

        /// <summary>
        /// Returns whether the <see cref="Compound"/> is an ion.
        /// </summary>
        public bool IsIon()
        {
            return Electrons != Protons;
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

            return other.SequenceEqual(other);
        }

        /// <summary>Serves as the default hash function. </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return this?.GetHashCode() ?? 0;
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var stack in this)
                sb.Append(stack);

            return sb.ToString();
        }

        #endregion
    }
}
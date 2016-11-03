using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nChem.Chemistry
{
    /// <summary>
    /// Represents a compound of atoms.
    /// </summary>
    public sealed class Compound : IList<Stack>, IAtomic
    {
        /// <summary>
        /// Initializes an instance of the <see cref="Compound"/> class.
        /// </summary>
        /// <param name="enumerable">An enumerable.</param>
        /// <param name="charge">The charge.</param>
        public Compound(IEnumerable<Stack> enumerable, int charge = 0)
        {
            var stacks = enumerable.ToList();

            Stacks = stacks;
            Electrons = Stacks.Sum(x => x.Size * x.Atom.Electrons) + -charge;
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

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        public IEnumerator<Stack> GetEnumerator()
        {
            return Stacks.GetEnumerator();
        }

        /// <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</exception>
        public void Add(Stack item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Stacks.Add(item);
        }

        /// <summary>Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only. </exception>
        public void Clear()
        {
            Stacks.Clear();
        }

        /// <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
        /// <returns>true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        public bool Contains(Stack item)
        {
            return Stacks.Any(x => x.Equals(item));
        }

        /// <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="arrayIndex" /> is less than 0.</exception>
        /// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
        public void CopyTo(Stack[] array, int arrayIndex)
        {
            Stacks.CopyTo(array, arrayIndex);
        }

        /// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
        /// <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</exception>
        public bool Remove(Stack item)
        {
            throw new NotImplementedException();
        }

        /// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
        public int Count => Stacks.Count;

        /// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</summary>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.</returns>
        public bool IsReadOnly => false;

        /// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1" />.</summary>
        /// <returns>The index of <paramref name="item" /> if found in the list; otherwise, -1.</returns>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1" />.</param>
        public int IndexOf(Stack item)
        {
            return Stacks.IndexOf(item);
        }

        /// <summary>Inserts an item to the <see cref="T:System.Collections.Generic.IList`1" /> at the specified index.</summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1" />.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.</exception>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1" /> is read-only.</exception>
        public void Insert(int index, Stack item)
        {
            Stacks.Insert(index, item);
        }

        /// <summary>Removes the <see cref="T:System.Collections.Generic.IList`1" /> item at the specified index.</summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.</exception>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1" /> is read-only.</exception>
        public void RemoveAt(int index)
        {
            Stacks.RemoveAt(index);
        }

        /// <summary>Gets or sets the element at the specified index.</summary>
        /// <returns>The element at the specified index.</returns>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.</exception>
        /// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.Generic.IList`1" /> is read-only.</exception>
        public Stack this[int index]
        {
            get { return Stacks[index]; }
            set { Stacks[index] = value; }
        }

        #region MyRegion

        /// <summary>
        /// Gets the stacks of the <see cref="Compound"/>.
        /// </summary>
        public List<Stack> Stacks { get; }

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
        public float? AtomicWeight => Stacks.Sum(x => x.AtomicWeight);

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
        public bool TryGetOxidationNumbers(out Dictionary<Stack, int> numbers)
        {
            int target = IsIon() ? ToIon().GetCharge() : 0;

            var results = new Dictionary<Stack, int>();
            Tuple<int, Element, Stack> unknown = null;

            int i = 0;
            foreach (var stack in Stacks)
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
                    case 1:
                        results.Add(stack, GetElements().Any(x => x.IsMetal) ? -1 : 1 * stack.Size);
                        continue;

                    case 8:
                        if (results.ContainsKey(atom.Element))
                        {
                            results[atom.Element] += -2*stack.Size;
                            continue;
                        }

                        results.Add(stack, -2 * stack.Size);
                        continue;

                    case 9:
                        results.Add(stack, -1 * stack.Size);
                        continue;
                }

                unknown = new Tuple<int, Element, Stack>(i - 1, atom.Element, stack);
            }

            if (unknown != null)
            {
                int x = byte.MaxValue;

                while (x + results.Sum(y => y.Value) != target)
                    x--;

                results[unknown.Item2] = x / unknown.Item3.Size;
            }

            numbers = results.ToDictionary(x => x.Key, y => y.Value / y.Key.Size);
            return numbers.Sum(x => x.Value * x.Key.Size) == target;
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

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace nChem.Chemistry
{
    /// <summary>
    /// Represents a collection of <see cref="Stack"/> objects.
    /// </summary>
    public sealed class StackCollection : List<Stack>
    {
        /// <summary>
        /// Initializes an instance of the <see cref="StackCollection"/> class.
        /// </summary>
        /// <param name="stacks">The stacks.</param>
        public StackCollection(IEnumerable<Stack> stacks)
            : base(stacks)
        { }

        /// <summary>
        /// Initializes an instance of the <see cref="StackCollection"/> class.
        /// </summary>
        public StackCollection()
        { }

        /// <summary>
        /// Appends a <see cref="Stack"/> with the specific element type- and size.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public StackCollection With(Element element, int size = 1)
        {
            Add(new Stack(element, size));
            return this;
        }
        
        /// <summary>
        /// Appends a specific array of <see cref="IAtomic"/> instances.
        /// </summary>
        /// <param name="items">The items to append.</param>
        /// <returns></returns>
        public StackCollection With(params IAtomic[] items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            AddRange(items.Select(x => new Stack(x)));
            return this;
        }

        /// <summary>
        /// Appends an array of <see cref="Stack"/> with the specific element type and a size of 1.
        /// </summary>
        /// <param name="elements">The elements to append.</param>
        /// <returns></returns>
        public StackCollection With(params Element[] elements)
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            AddRange(elements.Select(x => new Stack(x)));
            return this;
        }

        /// <summary>
        /// Determines whether the <see cref="StackCollection"/> contains a specific element.
        /// </summary>
        /// <param name="element">The element to find.</param>
        /// <returns></returns>
        public bool Contains(Element element)
        {
            return GetElements().Any(x => x.Equals(element));
        }

        /// <summary>
        /// Determines whether the <see cref="StackCollection"/> contains a stack with the specified element type- and minimum size.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="size">The minimum size.</param>
        /// <returns></returns>
        public bool Contains(Element element, int size)
        {
            return this.Any(x => x.Atom.GetElements().Count() == 1 && x.Atom.GetElements().First().Equals(element) && x.Size >= size);
        }

        /// <summary>
        /// Returns a collection of elements occurring in the <see cref="StackCollection"/>.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Element> GetElements()
        {
            return this.SelectMany(x => x.Atom.GetElements()).Distinct();
        }
    }
}
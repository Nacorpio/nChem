using System.Text;

namespace nChem.Chemistry
{
    /// <summary>
    /// Represents a quantity of an <see cref="Element"/>.
    /// </summary>
    public sealed class Stack
    {
        /// <summary>
        /// Initializes an instance of the <see cref="Stack"/> class.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="size">The size.</param>
        public Stack(Element element, int size = 1)
            : this(new Atom(element), size)
        { }

        /// <summary>
        /// Initializes an instance of the <see cref="Stack"/> class.
        /// </summary>
        /// <param name="atom">The element.</param>
        /// <param name="size">The size.</param>
        public Stack(IAtomic atom, int size = 1)
        {
            Atom = atom;
            Size = size;
        }

        /// <summary>
        /// Gets the element of the <see cref="Stack"/>.
        /// </summary>
        public IAtomic Atom { get; }

        /// <summary>
        /// Gets or sets the size of the <see cref="Stack"/>.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Returns the total atomic weight of the <see cref="Stack"/>.
        /// </summary>
        /// <returns></returns>
        public float? GetAtomicWeight()
        {
            return Atom.GetAtomicWeight() * Size;
        }

        /// <summary>
        /// Converts a specific <see cref="Element"/> to a stack.
        /// </summary>
        /// <param name="element">The element to convert.</param>
        public static implicit operator Stack(Element element)
        {
            return new Stack(new Atom(element));
        }

        /// <summary>
        /// Converts a specific <see cref="Atom"/> to a stack.
        /// </summary>
        /// <param name="atom">The atom to convert.</param>
        public static implicit operator Stack(Atom atom)
        {
            return new Stack(atom);
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Stack other = obj as Stack;
            return other != null
                && Atom.Equals(other.Atom)
                && Size.Equals(other.Size);
        }
        
        /// <summary>Serves as the default hash function. </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Atom != null ? Atom.GetHashCode() : 0) * 397) ^ Size;
            }
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            var atomic = Atom as Compound;
            if (atomic != null)
                sb.Append($"{Size}({atomic})");

            return sb.ToString();
        }
    }
}
namespace nChem
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
        {
            Element = element;
            Size = size;
        }

        /// <summary>
        /// Gets the element of the <see cref="Stack"/>.
        /// </summary>
        public Element Element { get; }

        /// <summary>
        /// Gets or sets the size of the <see cref="Stack"/>.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Returns the total atomic weight of the <see cref="Stack"/>.
        /// </summary>
        /// <returns></returns>
        public float GetAtomicWeight()
        {
            return Element.AtomicWeight*Size;
        }

        /// <summary>
        /// Determines whether the left-hand operand is equal to the right-hand operand.
        /// </summary>
        /// <param name="s1">The left-hand operand.</param>
        /// <param name="s2">The right-hand operand.</param>
        /// <returns></returns>
        public static bool operator ==(Stack s1, Stack s2)
        {
            return s1?.Element.AtomicNumber == s2?.Element.AtomicNumber && s1?.Size == s2?.Size;
        }

        /// <summary>
        /// Determines whether the left-hand operand is not equal to the right-hand operand.
        /// </summary>
        /// <param name="s1">The left-hand operand.</param>
        /// <param name="s2">The right-hand operand.</param>
        /// <returns></returns>
        public static bool operator !=(Stack s1, Stack s2)
        {
            return !(s1 == s2);
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{(Size == 1 ? string.Empty : Size.ToString())}{Element.Symbol}";
        }
    }
}
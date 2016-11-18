using System;

namespace nChem.Chemistry
{
    /// <summary>
    /// Represents a <see cref="RedoxEquation"/> delta type.
    /// </summary>
    public enum RedoxDeltaType
    {
        Oxidation,
        Reduction
    }

    public sealed class RedoxDelta
    {
        /// <summary>
        /// Initializes an instance of the <see cref="RedoxDelta"/> class.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="leftContent">The left-hand atomic content.</param>
        /// <param name="rightContent">The right-hand atomic content.</param>
        /// <param name="left">The left-hand oxidation number.</param>
        /// <param name="right">The right-hand oxidation number.</param>
        internal RedoxDelta(Element element, IAtomic leftContent, IAtomic rightContent, int left, int right)
        {
            Element = element;
            Left = new Tuple<IAtomic, int>(leftContent, left);
            Right = new Tuple<IAtomic, int>(rightContent, right);
        }

        /// <summary>
        /// Gets the element of the <see cref="RedoxDelta"/>.
        /// </summary>
        public Element Element { get; }

        /// <summary>
        /// Gets the left-hand side of the <see cref="RedoxDelta"/>.
        /// </summary>
        public Tuple<IAtomic, int> Left { get; }

        /// <summary>
        /// Gets the right-hand side of the <see cref="RedoxDelta"/>.
        /// </summary>
        public Tuple<IAtomic, int> Right { get; }

        /// <summary>
        /// Returns the result of the <see cref="RedoxDelta"/>.
        /// </summary>
        /// <returns></returns>
        public RedoxDeltaType GetResult()
        {
            return Left.Item2 > Right.Item2
                ? RedoxDeltaType.Reduction : RedoxDeltaType.Oxidation;
        }
    }
}
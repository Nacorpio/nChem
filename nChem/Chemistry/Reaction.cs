using System;
using System.Collections.Generic;

namespace nChem.Chemistry
{
    /// <summary>
    /// Represents an equation in which a reduction and oxidation is determined to form a balanced equation.
    /// </summary>
    public class RedoxEquation : Equation
    {
        /// <summary>
        /// Initializes an instance of the <see cref="Equation"/> class.
        /// </summary>
        /// <param name="left">The left-hand assignment.</param>
        /// <param name="right">The right-hand assignment.</param>
        public RedoxEquation(IEnumerable<Stack> left, IEnumerable<Stack> right) : base(left, right)
        { }

        /// <summary>
        /// Balances the <see cref="Equation"/>.
        /// </summary>
        /// <returns></returns>
        public override Equation Balance()
        {
            var queue = new Queue<Stack>(Left);
            while (queue.Count > 0)
            {
                Stack current = queue.Peek();
            }

            queue = new Queue<Stack>(Right);
            while (queue.Count > 0)
            {
                Stack current = queue.Peek();
            }

            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Represents a chemical equation.
    /// </summary>
    public abstract class Equation
    {
        /// <summary>
        /// Initializes an instance of the <see cref="Equation"/> class.
        /// </summary>
        /// <param name="left">The left-hand assignment.</param>
        /// <param name="right">The right-hand assignment.</param>
        protected Equation(IEnumerable<Stack> left, IEnumerable<Stack> right)
        {
            Left = left;
            Right = right;
        }

        /// <summary>
        /// Gets the left-hand assignment of the <see cref="Equation"/>.
        /// </summary>
        public IEnumerable<Stack> Left { get; }

        /// <summary>
        /// Gets the right-hand assignment of the <see cref="Equation"/>.
        /// </summary>
        public IEnumerable<Stack> Right { get; }

        /// <summary>
        /// Balances the <see cref="Equation"/>.
        /// </summary>
        /// <returns></returns>
        public abstract Equation Balance();
    }
}
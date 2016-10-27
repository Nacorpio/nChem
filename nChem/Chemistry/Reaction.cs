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
        public RedoxEquation(Compound left, Compound right) : base(left, right)
        {
        }

        /// <summary>
        /// Balances the <see cref="Equation"/>.
        /// </summary>
        /// <returns></returns>
        public override Equation Balance()
        {
            var queue = new Queue<Stack>(Left.Stacks);
            while (queue.Count > 0)
            {
                Stack current = queue.Peek();
            }

            queue = new Queue<Stack>(Right.Stacks);
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
        protected Equation(Compound left, Compound right)
        {
            Left = left;
            Right = right;
        }

        /// <summary>
        /// Gets the left-hand assignment of the <see cref="Equation"/>.
        /// </summary>
        public Compound Left { get; }

        /// <summary>
        /// Gets the right-hand assignment of the <see cref="Equation"/>.
        /// </summary>
        public Compound Right { get; }

        /// <summary>
        /// Balances the <see cref="Equation"/>.
        /// </summary>
        /// <returns></returns>
        public abstract Equation Balance();
    }
}
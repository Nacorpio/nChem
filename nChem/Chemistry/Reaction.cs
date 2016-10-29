using System;
using System.Collections.Generic;
using System.Linq;

namespace nChem.Chemistry
{
    /// <summary>
    /// Represents an equation in which a reduction and oxidation is determined to form a balanced equation.
    /// </summary>
    public sealed class RedoxEquation : Equation
    {
        /// <summary>
        /// Initializes an instance of the <see cref="Equation"/> class.
        /// </summary>
        /// <param name="left">The left-hand assignment.</param>
        /// <param name="right">The right-hand assignment.</param>
        public RedoxEquation(IEnumerable<Stack> left, IEnumerable<Stack> right) : base(left, right)
        { }

        /// <summary>
        /// Calculate the oxidation numbers of the specified <see cref="Stack"/> enumerable.
        /// </summary>
        /// <param name="stacks">The stacks.</param>
        /// <returns></returns>
        private List<Dictionary<Element, int>> GetOxidationNumbers(IEnumerable<Stack> stacks)
        {
            var results = new List<Dictionary<Element, int>>();

            var queue = new Queue<Stack>(stacks);
            while (queue.Count > 0)
            {
                Stack current = queue.Peek();

                if (!current.Atom.IsCompound())
                {
                    results.Add(new Dictionary<Element, int>
                    {
                        {current.Atom.GetElements().ToArray()[0], 0}
                    });

                    queue.Dequeue();
                    continue;
                }

                var compound = (Compound)current.Atom;

                Dictionary<Element, int> numbers;
                if (!compound.TryGetOxidationNumbers(out numbers))
                    throw new Exception("Couldn't retrieve oxidation numbers.");

                results.Add(numbers);
                queue.Dequeue();
            }

            return results;
        } 

        /// <summary>
        /// Balances the <see cref="Equation"/>.
        /// </summary>
        /// <returns></returns>
        public override bool Balance(out Equation equation)
        {
            List<Dictionary<Element, int>> left = GetOxidationNumbers(Left);
            List<Dictionary<Element, int>> right = GetOxidationNumbers(Right);

            equation = new RedoxEquation(null, null);
            return true;
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
        public abstract bool Balance(out Equation equation);
    }
}
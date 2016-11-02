using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        private IEnumerable<KeyValuePair<IAtomic, int[]>> GetOxidationNumbers(IEnumerable<Stack> stacks)
        {
            var results = new List<KeyValuePair<IAtomic, int[]>>();
            var queue = new Queue<Stack>(stacks.ToArray());

            while (queue.Count > 0)
            {
                Stack current = queue.Peek();

                if (!current.Atom.IsCompound())
                {
                    results.Add(new KeyValuePair<IAtomic, int[]>(current.Atom, new []{0}));
                    queue.Dequeue();

                    continue;
                }

                Compound compound = current.Atom as Compound;

                if (compound == null)
                    throw new Exception("Expected compound.");

                Dictionary<Element, int> numbers;
                if (!compound.TryGetOxidationNumbers(out numbers))
                    throw new Exception("Couldn't retrieve oxidation numbers.");

                results.Add(new KeyValuePair<IAtomic, int[]>(current.Atom, numbers.Select(x => x.Value).ToArray()));
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
            var left = GetOxidationNumbers(Left);
            var right = GetOxidationNumbers(Right);

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

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            Stack[] left = Left.ToArray();
            Stack[] right = Right.ToArray();

            for (var i = 0; i < left.Length; i++)
            {
                Stack current = left[i];
                if (i < left.Length - 1)
                {
                    sb.Append(current + " + ");
                    continue;
                }

                sb.Append(current + " -> ");
            }

            for (var i = 0; i < right.Length; i++)
            {
                Stack current = right[i];
                if (i < right.Length - 1)
                {
                    sb.Append(current + " + ");
                    continue;
                }

                sb.Append(current);
            }

            return sb.ToString();
        }
    }
}
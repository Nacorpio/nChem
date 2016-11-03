using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nChem.Chemistry
{
    /// <summary>
    ///     Represents an equation in which a reduction and oxidation is determined to form a balanced equation.
    /// </summary>
    public sealed class RedoxEquation : Equation
    {
        /// <summary>
        ///     Initializes an instance of the <see cref="Equation" /> class.
        /// </summary>
        /// <param name="left">The left-hand assignment.</param>
        /// <param name="right">The right-hand assignment.</param>
        public RedoxEquation(IEnumerable<Stack> left, IEnumerable<Stack> right) : base(left, right)
        {
        }

        /// <summary>
        ///     Calculate the oxidation numbers of the specified <see cref="Stack" /> enumerable.
        /// </summary>
        /// <param name="stacks">The stacks.</param>
        /// <returns></returns>
        private IEnumerable<KeyValuePair<IAtomic, int[]>> GetOxidationNumbers(IEnumerable<Stack> stacks)
        {
            var results = new List<KeyValuePair<IAtomic, int[]>>();
            var queue = new Stack<Stack>(stacks.ToArray());

            while (queue.Count > 0)
            {
                var current = queue.Peek();

                if (!current.Atom.IsCompound())
                {
                    var x = current.Atom.IsIon() ? current.Atom.ToIon().GetCharge() : 0;

                    results.Add(new KeyValuePair<IAtomic, int[]>(current.Atom, new[] {x}));
                    queue.Pop();

                    continue;
                }

                var compound = current.Atom as Compound;

                if (compound == null)
                    throw new Exception("Expected compound.");

                Dictionary<Stack, int> numbers;
                if (!compound.TryGetOxidationNumbers(out numbers))
                    throw new Exception("Couldn't retrieve oxidation numbers.");

                results.Add(new KeyValuePair<IAtomic, int[]>(current.Atom, numbers.Select(x => x.Value).ToArray()));
                queue.Pop();
            }

            return results;
        }

        /// <summary>
        /// Calculates the oxidation- and reduction deltas of the specified sides.
        /// </summary>
        /// <param name="left">The left-hand side.</param>
        /// <param name="right">The right-hand side.</param>
        /// <returns></returns>
        private IEnumerable<Tuple<Element, int, int>> GetDeltas(List<KeyValuePair<IAtomic, int[]>> left, List<KeyValuePair<IAtomic, int[]>> right)
        {
            if (left == null)
                throw new ArgumentNullException(nameof(left));

            if (right == null)
                throw new ArgumentNullException(nameof(right));

            var elements = left
                .SelectMany(x => x.Key.GetElements())
                .Concat(right
                    .SelectMany(x => x.Key.GetElements()))
                .Distinct()
                .ToArray();

            var deltas = new List<Tuple<Element, int, int>>(elements.Length);
            for (var i = 0; i < elements.Length; i++)
            {
                var element = elements[i];

                var leftFind =
                    left.FirstOrDefault(x => x.Key.GetElements().Any(y => Equals(y, element)));

                var rightFind =
                    right.FirstOrDefault(x => x.Key.GetElements().Any(y => Equals(y, element)));

                var leftNumber =
                    leftFind.Value.Reverse().ToArray()[
                        leftFind.Key.GetElements().ToList().FindIndex(x => x.Equals(element))];

                var rightNumber =
                    rightFind.Value.Reverse().ToArray()[
                        rightFind.Key.GetElements().ToList().FindIndex(x => x.Equals(element))];

                deltas.Add(new Tuple<Element, int, int>(element, leftNumber, rightNumber));
            }

            return deltas;
        }

        /// <summary>
        ///     Balances the <see cref="Equation" />.
        /// </summary>
        /// <returns></returns>
        public override bool Balance(out Equation equation)
        {
            IEnumerable<KeyValuePair<IAtomic, int[]>> left = GetOxidationNumbers(LReactant).ToArray();
            IEnumerable<KeyValuePair<IAtomic, int[]>> right = GetOxidationNumbers(RReactant).ToArray();

            Tuple<Element, int, int>[] deltas = GetDeltas(left.ToList(), right.ToList()).ToArray();

            var oxidations = deltas.Where(x => x.Item2 < x.Item3);
            var reductions = deltas.Where(x => x.Item2 > x.Item3);

            equation = new RedoxEquation(null, null);
            return true;
        }
    }

    /// <summary>
    ///     Represents a chemical equation.
    /// </summary>
    public abstract class Equation
    {
        /// <summary>
        ///     Initializes an instance of the <see cref="Equation" /> class.
        /// </summary>
        /// <param name="r0">The left-hand reactant.</param>
        /// <param name="r1">The right-hand reactant.</param>
        protected Equation(IEnumerable<Stack> r0, IEnumerable<Stack> r1)
        {
            LReactant = r0;
            RReactant = r1;
        }

        /// <summary>
        ///     Gets the left-hand assignment of the <see cref="Equation" />.
        /// </summary>
        public IEnumerable<Stack> LReactant { get; }

        /// <summary>
        ///     Gets the right-hand assignment of the <see cref="Equation" />.
        /// </summary>
        public IEnumerable<Stack> RReactant { get; }

        /// <summary>
        ///     Balances the <see cref="Equation" />.
        /// </summary>
        /// <returns></returns>
        public abstract bool Balance(out Equation equation);

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            var left = LReactant.ToArray();
            var right = RReactant.ToArray();

            for (var i = 0; i < left.Length; i++)
            {
                var current = left[i];
                if (i < left.Length - 1)
                {
                    sb.Append(current + " + ");
                    continue;
                }

                sb.Append(current + " -> ");
            }

            for (var i = 0; i < right.Length; i++)
            {
                var current = right[i];
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
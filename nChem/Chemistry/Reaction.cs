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
        /// <param name="reactants">The reactants.</param>
        /// <param name="products">The products.</param>
        public RedoxEquation(IEnumerable<Stack> reactants, IEnumerable<Stack> products) : base(reactants, products)
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
        /// <param name="reactants">The left-hand side.</param>
        /// <param name="products">The right-hand side.</param>
        /// <returns></returns>
        private IEnumerable<Tuple<Element, int, int>> GetDeltas(List<KeyValuePair<IAtomic, int[]>> reactants, List<KeyValuePair<IAtomic, int[]>> products)
        {
            if (reactants == null)
                throw new ArgumentNullException(nameof(reactants));

            if (products == null)
                throw new ArgumentNullException(nameof(products));

            var elements = reactants
                .SelectMany(x => x.Key.GetElements())
                .Concat(products
                    .SelectMany(x => x.Key.GetElements()))
                .Distinct()
                .ToArray();

            var deltas = new List<Tuple<Element, int, int>>(elements.Length);
            for (var i = 0; i < elements.Length; i++)
            {
                var element = elements[i];

                var leftFind =
                    reactants.FirstOrDefault(x => x.Key.GetElements().Any(y => Equals(y, element)));

                var rightFind =
                    products.FirstOrDefault(x => x.Key.GetElements().Any(y => Equals(y, element)));

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
            /*
                Balancing Redox equations in acidic solution:   

                Step #1: Determine the oxidation numbers of all elements in the equation.
                Step #2: Determine which elements are being oxidized and which are being reduced.
                Step #3: Form half-reactions from the oxidized- and reduced element.

                Step #4: Balance the half-reactions by both charge and element quantity.
                    Do the following for both the reduction- and oxidation half-reactions:
                        1: Determine the element deltas.
                        2: Add H2O on unbalanced Oxygens and add a positive Hydrogen ion on unbalanced Hydrogens.
                        3: Determine the charge deltas. 
                        4: Add electrons on the unbalanced side in order to match the charges.
                        5: Balance the electrons by multiplying the half-reactions with the least amount of electrons by a certain number.
                        6: Eliminate any electrons that cancel out.
                        7: Simplify half-reactions that can be further simplified.

                Step #5: Combine the two half-reactions from left to right.
                Step #6: Confirm that both sides are properly balanced.
            */

            // Calculate the oxidation numbers for the reactants and products.
            IEnumerable<KeyValuePair<IAtomic, int[]>> reactants = GetOxidationNumbers(Reactants).ToArray();
            IEnumerable<KeyValuePair<IAtomic, int[]>> products = GetOxidationNumbers(Products).ToArray();

            // Calculate the oxidation- and reduction deltas.
            Tuple<Element, int, int>[] deltas = GetDeltas(reactants.ToList(), products.ToList()).ToArray();

            // Organize the oxidations and reductions in two separate enumerables.
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
        /// <param name="reactants">The reactants.</param>
        /// <param name="products">The products.</param>
        protected Equation(IEnumerable<Stack> reactants, IEnumerable<Stack> products)
        {
            Reactants = reactants;
            Products = products;
        }

        /// <summary>
        ///     Gets the left-hand assignment (reactants) of the <see cref="Equation" />.
        /// </summary>
        public IEnumerable<Stack> Reactants { get; }

        /// <summary>
        ///     Gets the right-hand assignment (products) of the <see cref="Equation" />.
        /// </summary>
        public IEnumerable<Stack> Products { get; }

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

            var left = Reactants.ToArray();
            var right = Products.ToArray();

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
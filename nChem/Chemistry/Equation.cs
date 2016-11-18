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
        public RedoxEquation(StackCollection reactants, StackCollection products) : base(reactants, products)
        {
        }

        /// <summary>
        ///     Calculate the oxidation numbers of the specified <see cref="Stack" /> enumerable.
        /// </summary>
        /// <param name="stacks">The stacks.</param>
        /// <returns></returns>
        private IEnumerable<OxidationResult> GetOxidationNumbers(IEnumerable<Stack> stacks)
        {
            var results = new List<OxidationResult>();
            var queue = new Stack<Stack>(stacks.ToArray());

            while (queue.Count > 0)
            {
                var current = queue.Peek();

                if (!current.Atom.IsCompound())
                {
                    results.Add(new OxidationResult(current.Atom, new Dictionary<Element, int>
                    {
                        {current.Atom.GetElements().First(), current.Atom.IsIon() ? current.Atom.ToIon().GetCharge() : 0}
                    }));

                    queue.Pop();
                    continue;
                }

                var compound = current.Atom as Compound;

                if (compound == null)
                    throw new Exception("Expected compound.");

                Dictionary<Stack, int> numbers;
                if (!compound.TryGetOxidationNumbers(out numbers))
                    throw new Exception("Couldn't retrieve oxidation numbers.");

                results.Add(new OxidationResult(current.Atom, numbers.Reverse().ToDictionary(x => x.Key.Atom.GetElements().First(), x => x.Value)));
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
        private IEnumerable<RedoxDelta> GetDeltas(List<OxidationResult> reactants, List<OxidationResult> products)
        {
            if (reactants == null)
                throw new ArgumentNullException(nameof(reactants));

            if (products == null)
                throw new ArgumentNullException(nameof(products));
            
            var elements = reactants
                .SelectMany(x => x.Content.GetElements())
                .Concat(products.SelectMany(x => x.Content.GetElements()))
                .Distinct()
                .ToArray();

            var deltas = new List<RedoxDelta>(elements.Length);
            foreach (var e in elements)
            {
                var leftFind =
                    reactants.FirstOrDefault(x => x.Content.GetElements().Contains(e));

                var rightFind =
                    products.FirstOrDefault(x => x.Content.GetElements().Contains(e));

                if (leftFind == null || rightFind == null)
                    throw new NullReferenceException();

                if (leftFind.Numbers[leftFind.Content.GetElements().First(x => x.Equals(e))] ==
                    rightFind.Numbers[rightFind.Content.GetElements().First(x => x.Equals(e))])
                    continue;

                deltas.Add(new RedoxDelta(e, leftFind.Content, rightFind.Content, leftFind.Numbers[leftFind.Content.GetElements().First(x => x.Equals(e))], rightFind.Numbers[rightFind.Content.GetElements().First(x => x.Equals(e))]));
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
            IEnumerable<OxidationResult> reactants = GetOxidationNumbers(Reactants).ToArray();
            IEnumerable<OxidationResult> products = GetOxidationNumbers(Products).ToArray();

            // Calculate the oxidation- and reduction deltas.
            RedoxDelta[] deltas = GetDeltas(reactants.ToList(), products.ToList()).ToArray();

            // Organize the oxidations and reductions in two separate enumerable instances.
            RedoxDelta oxidation = deltas.First(x => x.GetResult() == RedoxDeltaType.Oxidation);
            RedoxDelta reduction = deltas.First(x => x.GetResult() == RedoxDeltaType.Reduction);

            int oxDelta = Math.Abs(oxidation.Right.Item2 - oxidation.Left.Item2);
            int redDelta = Math.Abs(reduction.Right.Item2 - oxidation.Left.Item2);
            
            // The least common multiple.
            int lcm = Math.Abs(MathUtils.Lcm(oxDelta, redDelta));

            // We now have the multipliers.
            int oxMultiplier = lcm/oxDelta;
            int redMultiplier = lcm/redDelta;

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
        protected Equation(StackCollection reactants, StackCollection products)
        {
            Reactants = reactants;
            Products = products;
        }

        /// <summary>
        ///     Gets the left-hand assignment (reactants) of the <see cref="Equation" />.
        /// </summary>
        public StackCollection Reactants { get; }

        /// <summary>
        ///     Gets the right-hand assignment (products) of the <see cref="Equation" />.
        /// </summary>
        public StackCollection Products { get; }

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
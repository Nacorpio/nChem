using System.Linq;
using System.Text;

namespace nChem
{
    /// <summary>
    /// Represents a compound of atoms.
    /// </summary>
    public class Compound
    {
        public static Compound
            Bronze = new Compound(new Stack(Element.Copper), new Stack(Element.Tin));

        /// <summary>
        /// Initializes an instance of the <see cref="Compound"/> class.
        /// </summary>
        /// <param name="stacks">The atom stacks.</param>
        public Compound(params Stack[] stacks)
        {
            Stacks = stacks;
        }

        /// <summary>
        /// Gets the stacks of the <see cref="Compound"/>.
        /// </summary>
        public Stack[] Stacks { get; }

        /// <summary>
        /// Returns the total atomic weight of the <see cref="Compound"/>.
        /// </summary>
        /// <returns></returns>
        public float GetAtomicWeight()
        {
            return Stacks.Sum(x => x.GetAtomicWeight());
        }

        /// <summary>
        /// Returns an array of all elements that the <see cref="Compound"/> contains.
        /// </summary>
        /// <returns></returns>
        public Element[] GetElements()
        {
            return Stacks.Select(x => x.Element)
                .Distinct()
                .ToArray();
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var stack in Stacks)
                sb.Append(stack);

            return sb.ToString();
        }
    }
}
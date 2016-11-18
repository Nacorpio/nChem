using System.Collections.Generic;

namespace nChem.Chemistry
{
    public sealed class OxidationResult
    {
        /// <summary>
        /// Initializes an instance of the <see cref="OxidationResult"/> class.
        /// </summary>
        /// <param name="content">The atomic content.</param>
        /// <param name="numbers">The oxidation numbers.</param>
        internal OxidationResult(IAtomic content, Dictionary<Element, int> numbers)
        {
            Content = content;
            Numbers = numbers;
        }

        /// <summary>
        /// Gets the content of the <see cref="OxidationResult"/>.
        /// </summary>
        public IAtomic Content { get; }

        /// <summary>
        /// Gets the oxidation numbers of the <see cref="OxidationResult"/>.
        /// </summary>
        public Dictionary<Element, int> Numbers { get; }
    }
}
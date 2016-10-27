namespace nChem.Units
{
    public static partial class UnitRepository
    {
        public sealed class Prefix
        {
            /// <summary>
            /// Initializes an instance of the <see cref="Prefix"/> class.
            /// </summary>
            /// <param name="name">The name.</param>
            /// <param name="symbol">The symbol.</param>
            /// <param name="multiplier">The multiplier.</param>
            /// <param name="kind">The prefix kind.</param>
            internal Prefix(string name, char symbol, float multiplier, PrefixKind kind)
            {
                Name = name;
                Symbol = symbol;
                Multiplier = multiplier;
                Kind = kind;
            }

            /// <summary>
            /// Gets the symbol of the <see cref="Prefix"/>.
            /// </summary>
            public char Symbol { get; }

            /// <summary>
            /// Gets the name of the <see cref="Prefix"/>.
            /// </summary>
            public string Name { get; }

            /// <summary>
            /// Gets the multiplier of the <see cref="Prefix"/>.
            /// </summary>
            public float Multiplier { get; }

            /// <summary>
            /// Gets the kind of the <see cref="Prefix"/>.
            /// </summary>
            public PrefixKind Kind { get; }
        }
    }
}
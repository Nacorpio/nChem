using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nChem
{
    public static class FormatUtils
    {
        public static Dictionary<char, char> Superscripts = new Dictionary<char, char>
        {
            {'⁰', '0'},
            {'¹', '1'},
            {'²', '2'},
            {'³', '3'},
            {'⁴', '4'},
            {'⁵', '5'},
            {'⁶', '6'},
            {'⁷', '7'},
            {'⁸', '8'},
            {'⁹', '9'},
            {'⁺', '+'},
            {'⁻', '-'}
        };

        public static Dictionary<char, char> Subscripts = new Dictionary<char, char>
        {
            {'₀', '0'},
            {'₁', '1'},
            {'₂', '2'},
            {'₃', '3'},
            {'₄', '4'},
            {'₅', '5'},
            {'₆', '6'},
            {'₇', '7'},
            {'₈', '8'},
            {'₉', '9'},
            {'₊', '+'},
            {'₋', '-'}
        };

        public static short ToInt16(char[] chars)
        {
            var sb = new StringBuilder();

            foreach (var c in chars)
            {
                if (Superscripts.ContainsKey(c))
                {
                    sb.Append(Superscripts[c]);
                    continue;
                }

                if (Subscripts.ContainsKey(c))
                    sb.Append(Subscripts[c]);
            }

            return short.Parse(sb.ToString());
        }

        public static short ToInt16(string input)
        {
            return ToInt16(input.ToCharArray());
        }

        public static short ToInt16(char c)
        {
            var sb = new StringBuilder();

            if (Superscripts.ContainsKey(c))
                sb.Append(Superscripts[c]);

            if (Subscripts.ContainsKey(c))
                sb.Append(Subscripts[c]);

            return short.Parse(sb.ToString());
        }
        
        public static int ToInt32(char c)
        {
            return ToInt16(c);
        }

        public static long ToInt64(char c)
        {
            return ToInt32(c);
        }
    }

    public static class MathUtils
    {
        /// <summary>
        /// Converts the specified prefix to a float value.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <returns></returns>
        public static float ToFloat(PrefixKind prefix)
        {
            switch (prefix)
            {
                case PrefixKind.None:
                    return 1e0f;

                case PrefixKind.Deka:
                    return 1e1f;

                case PrefixKind.Hekto:
                    return 1e2f;

                case PrefixKind.Kilo:
                    return 1e3f;

                case PrefixKind.Mega:
                    return 1e6f;

                case PrefixKind.Giga:
                    return 1e9f;

                case PrefixKind.Tera:
                    return 1e12f;

                case PrefixKind.Peta:
                    return 1e15f;

                case PrefixKind.Exa:
                    return 1e18f;

                case PrefixKind.Deci:
                    return 1e-1f;

                case PrefixKind.Centi:
                    return 1e-2f;

                case PrefixKind.Milli:
                    return 1e-3f;

                case PrefixKind.Mikro:
                    return 1e-6f;

                case PrefixKind.Nano:
                    return 1e-9f;

                case PrefixKind.Piko:
                    return 1e-12f;

                case PrefixKind.Femto:
                    return 1e-15f;

                case PrefixKind.Atto:
                    return 1e-18f;

                case PrefixKind.Zepto:
                    return 1e-21f;

                default:
                    return -1;
            }
        }
    }

    public static class ChemistryUtils
    {
        /// <summary>
        /// Gets the characters representing shells within an atom.
        /// </summary>
        public static char[] ShellLabels = {'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u'};

        /// <summary>
        /// Gets the characters representing the labels of subshells inside of a shell.
        /// </summary>
        public static char[] SubshellLabels = {'s', 'p', 'd', 'f'};

        public static Dictionary<char, char[]> EnergyLevelConfiguration = new Dictionary<char, char[]>
        {
            {'k', new[] {'s'}},
            {'l', new [] {'s', 'p'}},
            {'m', new[] {'s', 'p', 'd'}},
            {'n', new[] {'s', 'p', 'd', 'f'}},
        };

        /// <summary>
        /// Returns the zero-based index of a shell with the specified label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns></returns>
        public static int GetShellIndex(char label)
        {
            return ShellLabels.ToList().FindIndex(x => x == label);
        }

        /// <summary>
        /// Returns the zero-based index of a subshell with the specified label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns></returns>
        public static int GetSubshellIndex(char label)
        {
            return SubshellLabels.ToList().FindIndex(x => x == label);
        }

        /// <summary>
        /// Returns the amount of electrons that can be stored within shell <c>n</c>.
        /// </summary>
        /// <param name="n">The zero-based index of a shell.</param>
        /// <returns></returns>
        public static int GetShellCapacity(int n)
        {
            return (int) (2 * Math.Pow(n + 1, 2));
        }

        /// <summary>
        /// Returns the amount of shells needed to store a specific amount of electrons.
        /// </summary>
        /// <param name="electronCount"></param>
        /// <returns></returns>
        public static int GetShellCount(int electronCount)
        {
            int result = 0;

            if (electronCount >= 1)
                result++;

            if (electronCount >= 4)
                result++;

            if (electronCount >= 9)
                result++;

            if (electronCount >= 16)
                result++;

            if (electronCount >= 25)
                result++;

            if (electronCount >= 36)
                result++;

            if (electronCount >= 49)
                result++;

            if (electronCount >= 64)
                result++;

            if (electronCount >= 81)
                result++;

            if (electronCount >= 100)
                result++;

            return result;
        }
    }
}

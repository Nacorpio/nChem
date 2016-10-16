using System;
using System.Collections.Generic;
using System.Linq;

namespace nChem
{
    public static class MathUtilities
    {
        /// <summary>
        /// Gets the characters representing shells within an atom.
        /// </summary>
        public static char[] ShellLabels = {'k', 'l', 'm', 'n', 'o', 'p', 'q'};

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

            return result;
        }
    }
}

using System.Collections.Generic;

namespace nChem
{
    public static class IntegerExtensions
    {
        public static IEnumerable<int> To(this int first, int last)
        {
            for (int i = first; i <= last; i++)

            {
                yield return i;
            }
        }
    }
}

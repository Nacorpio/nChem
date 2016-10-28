using System.Linq;
using nChem.Chemistry;

namespace nChem.Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            var compound = new Compound(new[]
            {
                new Stack(Element.Chlorine, 2),
                new Stack(Element.Oxygen),
            });

            int[] numbers;
            if (compound.TryGetOxidationNumbers(out numbers))
            {
                int total = numbers.Sum(x => x);
            }
        }
    }
}
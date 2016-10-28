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
                new Stack(Element.Hydrogen, 2),
                new Stack(Element.Sulfur),
                new Stack(Element.Oxygen, 4), 
            });

            int[] numbers;
            if (compound.TryGetOxidationNumbers(out numbers))
            {
                int total = numbers.Sum(x => x);
            }
        }
    }
}
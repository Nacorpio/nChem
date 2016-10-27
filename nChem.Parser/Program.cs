using nChem.Chemistry;

namespace nChem.Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            Compound c = new Compound(new []
            {
                new Stack(Element.Nitrogen, 2),
                new Stack(Element.Oxygen),  
            });

            var result = c.GetOxidationNumbers();
        }
    }
}
using nChem.Chemistry;

namespace nChem.Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            var equation = new RedoxEquation
            (
                new Stack[]
                {
                    new Atom(Element.Calcium),
                    new Stack(Element.Chlorine, 2),
                },
                new Stack[]
                {
                    new Compound(new Stack[]
                    {
                        new Atom(Element.Calcium),
                        new Stack(Element.Chlorine, 2),
                    }),
                }
            );

            Equation x;
            if (equation.Balance(out x))
            {
                
            }
        }
    }
}
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
                    new Atom(Element.Silver),
                    new Compound(new Stack[]
                    {
                        new Atom(Element.Nitrogen),
                        new Stack(Element.Oxygen, 3),  
                    }, -1), 
                },
                new Stack[]
                {
                    new Atom(Element.Silver, 1),
                    new Compound(new Stack[]
                    {
                        new Atom(Element.Nitrogen),
                        new Atom(Element.Oxygen), 
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
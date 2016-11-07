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
                    new Compound(new Stack[]
                    {
                        new Stack(Element.Iron, 2),
                        new Stack(Element.Oxygen, 3),  
                    }), 
                    new Compound(new Stack[]
                    {
                        new Atom(Element.Carbon),
                        new Atom(Element.Oxygen),  
                    }), 
                },
                new Stack[]
                {
                    new Atom(Element.Iron),
                    new Compound(new Stack[]
                    {
                        new Atom(Element.Carbon),
                        new Stack(Element.Oxygen, 2), 
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
using System;
using nChem.Chemistry;
using nChem.Chemistry.Energy;

namespace nChem.Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            var atom = new Atom(Element.Copper);
            ShellConfiguration configuration = atom.GetShellConfiguration();

            Console.WriteLine(configuration);
            Console.ReadLine();
        }
    }
}
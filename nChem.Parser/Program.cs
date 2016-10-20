using nChem.Units;

namespace nChem.Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            var atom = new Atom(Element.Carbon);
            ShellConfiguration configuration = atom.GetShellConfiguration();
        }
    }
}
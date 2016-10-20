using nChem.Units;

namespace nChem.Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            Atom atom = new Atom(Element.Phosphorus);
            ShellConfiguration configuration = atom.GetShellConfiguration();
        }
    }
}
using nChem.Units;

namespace nChem.Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            Atom atom = new Atom(Element.Fluorine);
            ShellConfiguration configuration = atom.GetShellConfiguration();
        }
    }
}
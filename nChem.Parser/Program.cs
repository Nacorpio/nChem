using System.Collections.Generic;

namespace nChem.Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            var atom = new Atom(Element.Carbon);
            Shell valenceShell = atom.GetShellConfiguration().GetValenceShell();

            ShellConfiguration configuration = atom.GetShellConfiguration();
            IEnumerable<Electron> electrons = configuration.GetElectrons();
        }
    }
}
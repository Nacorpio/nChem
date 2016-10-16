// ReSharper disable AssignNullToNotNullAttribute

using System;

namespace nChem.Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            Atom atom = new Atom(Element.Aluminium);
            ShellConfiguration configuration = atom.GetShellConfiguration();

            Shell[] shells = configuration.ToArray();

            Console.WriteLine($"Electron configuration for {atom.Element}:");

            foreach (var shell in shells)
            {
                Console.Write(shell + " ");
            }

            Console.ReadLine();
        }
    }
}

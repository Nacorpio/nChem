using System.IO;

namespace nChem.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var elements = Element.Parse(File.ReadAllText("elements.json"));
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace nChem.Chemistry
{
    public partial class Element
    {
        private static List<Element> _cachedElements;

        /// <summary>
        /// Parses a collection of elements from the specified JSON-formatted string.
        /// </summary>
        /// <param name="json">The JSON-formatted string.</param>
        /// <returns></returns>
        public static IEnumerable<Element> Parse(string json)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Element>>(json);
        }

        /// <summary>
        /// Parses a collection of elements from a local JSON-formatted file.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Element> Parse()
        {
            return Parse(File.ReadAllText("elements.json"));
        }

        public static void FetchLenntech()
        {
            var results = new List<Element>();
            if (File.Exists("elements.json"))
            {
                _cachedElements = new List<Element>(Parse());
                return;
            }

            using (var wc = new WebClient())
            {
                const string elementsUrl = "http://www.lenntech.com/periodic-chart-elements/atomic-mass.htm";
                const string elementsPath = "*//tr/td/a[@href]/parent::node()/parent::node()";

                string src = wc.DownloadString(elementsUrl);
                var doc = new HtmlDocument();

                doc.LoadHtml(src);

                HtmlNode root = doc.DocumentNode;
                HtmlNodeCollection query = root.SelectNodes(elementsPath);

                var nodes = new List<HtmlNode>(query);
                foreach (var node in nodes)
                {
                    IEnumerable<HtmlNode> all = node.ChildNodes
                        .Where(x => x.Name != "#text").ToList();

                    IEnumerable<HtmlNode> td = all
                        .Where(x => x.Name == "td")
                        .Where(x => x.HasAttributes)
                        .Where(x => x.Attributes.Contains("class"));

                    HtmlNode align = all
                        .FirstOrDefault(x => x.Attributes.Contains("class"));

                    if (align == null)
                        throw new Exception("Couldn't parse the atomic number.");

                    List<HtmlNode> properties = td.ToList();

                    string url = properties.Count == 5 ? properties[2].FirstChild.Attributes["href"].Value : properties[1].FirstChild.Attributes["href"].Value;
                    string name = properties.Count == 5 ? properties[2].InnerText : properties[1].InnerText;
                    string symbol = properties.Count == 5 ? properties[3].InnerText : properties[2].InnerText;

                    float? weight;

                    if (!string.IsNullOrWhiteSpace(properties[0].InnerText))
                        weight = float.Parse(properties.Count == 5 ? properties[1].InnerText : properties[0].InnerText);
                    else
                        weight = null;

                    results.Add(new Element(int.Parse(properties[properties.Count - 1].InnerText))
                    {
                        Name = name,
                        Symbol = symbol,
                        AtomicWeight = weight,
                    });
                }
            }
            _cachedElements = results;
        }

        public static void FetchScienceIl()
        {
            var results = new List<Element>();
            if (File.Exists("elements.json"))
            {
                _cachedElements = new List<Element>(Parse());
                return;
            }

            using (var wc = new WebClient())
            {
                const string elementsUrl = "http://www.science.co.il/PTelements.asp?s=Weight";
                const string elementsPath = "//table[@class='tabint8']/tr";

                string src = wc.DownloadString(elementsUrl);
                var doc = new HtmlDocument();

                doc.LoadHtml(src);

                HtmlNode root = doc.DocumentNode;
                HtmlNodeCollection xPathQuery = root.SelectNodes(elementsPath);

                xPathQuery.RemoveAt(0);
                xPathQuery.RemoveAt(xPathQuery.Count - 1);

                foreach (var node in xPathQuery)
                {
                    Chemistry.Element element = new Chemistry.Element();
                    HtmlNode[] properties = node.Elements("td").ToArray();

                    element.AtomicWeight = float.Parse(properties[2].InnerText);
                    element.Name = properties[3].InnerText;
                    element.Symbol = properties[4].InnerText;
                    element.MeltingPoint = float.Parse(string.IsNullOrEmpty(properties[5].InnerText) ? "0" : properties[5].InnerText);
                    element.BoilingPoint = float.Parse(string.IsNullOrEmpty(properties[6].InnerText) ? "0" : properties[6].InnerText);
                    element.Density = float.Parse(string.IsNullOrEmpty(properties[7].InnerText) ? "0" : properties[7].InnerText);
                    element.AbundanceInEarth = float.Parse(string.IsNullOrEmpty(properties[8].InnerText) ? "0" : properties[8].InnerText);
                    element.Group = int.Parse(properties[10].InnerText);
                    element.FirstIonizationEnergy = float.Parse(string.IsNullOrEmpty(properties[12].InnerText) ? "0" : properties[12].InnerText);

                    results.Add(element);
                }
            }

            _cachedElements = new List<Element>(results);
        }

        /// <summary>
        /// Fetches a collection of elements from <c>periodni.com</c>.
        /// </summary>
        /// <returns></returns>
        public static void FetchPeriodni()
        {
            var results = new List<Element>();
            if (File.Exists("elements.json"))
            {
                _cachedElements = new List<Element>(Parse());
                return;
            }

            using (var wc = new WebClient())
            {
                const string elementsUrl = "http://www.periodni.com/elements_names_sorted_alphabetically.html";
                const string elementPath = "//div[@class='xrow']//tr/td/parent::tr";

                var src = wc.DownloadString(elementsUrl);
                var doc = new HtmlDocument();

                doc.LoadHtml(src);

                var node = doc.DocumentNode;
                var elementNodes = node.SelectNodes(elementPath);

                foreach (var elementNode in elementNodes)
                {
                    var tds = new List<HtmlNode>(elementNode.Elements("td"));
                    var element = new Element(int.Parse(tds[0].InnerText))
                    {
                        Symbol = tds[1].InnerText
                    };

                    string url = "http://www.periodni.com/" + tds[1].FirstChild.Attributes["href"].Value;
                    element.Name = tds[2].InnerText;

                    src = wc.DownloadString(url);
                    doc = new HtmlDocument();

                    doc.LoadHtml(src);
                    node = doc.DocumentNode;

                    element.Category = node.SelectSingleNode("//*[@id=\"ebody\"]/p[1]").InnerText
                        .ToLower()
                        .Replace(' ', '-')
                        .Replace(":", string.Empty);

                    var generalNodes = node.SelectSingleNode("//div[@id='general']").Descendants("tr");
                    var physicalNodes = node.SelectSingleNode("//div[@id='physical']").Descendants("tr");
                    var thermalNodes = node.SelectSingleNode("//div[@id='thermal']").Descendants("tr");
                    var ionizationNodes = node.SelectSingleNode("//div[@id='ionization']").Descendants("tr");
                    var abundanceNodes = node.SelectSingleNode("//div[@id='abundance']").Descendants("tr");
                    var crystalNodes = node.SelectSingleNode("//div[@id='crystal']").Descendants("tr");

                    var propertyNodes = physicalNodes
                        .Concat(generalNodes)
                        .Concat(thermalNodes)
                        .Concat(ionizationNodes)
                        .Concat(abundanceNodes)
                        .Concat(crystalNodes);

                    foreach (var propertyNode in propertyNodes)
                    {
                        string key = propertyNode.ChildNodes[0].InnerText
                            .Replace(":", string.Empty)
                            .Replace(' ', '-')
                            .ToLower();

                        string value = propertyNode.ChildNodes[1].InnerText
                            .Replace("~", string.Empty);

                        if (value == "-")
                            value = "-1";

                        switch (key)
                        {
                            case "relative-atomic-mass":

                                var splitValue = value.Split(' ');
                                element.AtomicWeight = float.Parse(
                                    Regex.Replace(splitValue.Length == 1 ? value : splitValue[0], "\\(\\d\\)", string.Empty)
                                    .Replace("[", string.Empty)
                                    .Replace(",", string.Empty));

                                break;

                            case "group-numbers":
                                element.Group = int.Parse(value);
                                break;

                            case "period":
                                element.Period = int.Parse(value);
                                break;

                            case "electronic-configuration":
                                element.ElectronConfiguration = value;
                                break;

                            case "molar-volume-/-cm3mol-1":
                                element.MolarVolume = float.Parse(value);
                                break;

                            case "electronegativities":
                                element.Electronegativity = float.Parse(value);
                                break;

                            case "atomic-radius-/-pm":
                                element.AtomicRadius = float.Parse(value);
                                break;

                            case "density-/-g-dm-3":
                                element.Density = float.Parse(value);
                                break;

                            case "thermal-conductivity-/-w-m-1k-1":
                                element.ThermalConductivity = value;
                                break;

                            case "melting-point-/-&deg;c":
                                element.MeltingPoint = float.Parse(value);
                                break;

                            case "boiling-point-/-&deg;c":
                                element.BoilingPoint = float.Parse(value);
                                break;

                            case "heat-of-fusion-/-kj-mol-1":
                                element.HeatOfFusion = float.Parse(value);
                                break;

                            case "heat-of-vaporization-/-kj-mol-1":
                                element.HeatOfVaporization = float.Parse(value);
                                break;

                            case "heat-of-atomization-/-kj-mol-1":
                                element.HeatOfAtomization = float.Parse(value);
                                break;

                            case "first-ionization-energy-/-kj-mol-1":
                                element.FirstIonizationEnergy = float.Parse(value);
                                break;

                            case "second-ionization-energy-/-kj-mol-1":
                                element.SecondIonizationEnergy = float.Parse(value);
                                break;

                            case "third-ionization-energy-/-kj-mol-1":
                                element.ThirdIonizationEnergy = float.Parse(value);
                                break;

                            case "in-the-atmosphere-/-ppm":
                                element.AbundanceInAtmosphere = value;
                                break;

                            case "in-the-oceans-/-ppm":
                                element.AbundanceInOceans = value;
                                break;

                            case "in-the-earth's-crust-/-ppm":
                                element.AbundanceInEarth = float.Parse(value);
                                break;

                            case "crystal-structure":
                                element.CrystalStructure = value;
                                break;

                            case "unit-cell-dimensions-/-pm":
                                element.UnitCellDimensions = value;
                                break;

                            case "space-group":
                                element.SpaceGroup = value;
                                break;
                        }
                    }

                    results.Add(element);
                }

                _cachedElements = results;
            }
        }

        /// <summary>
        /// Returns the greatest atomic number within the fetched collection of elements.
        /// </summary>
        /// <returns></returns>
        public static int GetMaxAtomicNumber()
        {
            return _cachedElements.Max(x => x.AtomicNumber);
        }

        /// <summary>
        /// Returns an element with a specific atomic number.
        /// </summary>
        /// <param name="atomicNumber">The atomic number of the element to find.</param>
        /// <returns></returns>
        public static Element GetElement(int atomicNumber)
        {
            return _cachedElements.FirstOrDefault(x => x.AtomicNumber == atomicNumber);
        }

        /// <summary>
        /// Returns a cached collection of chemical elements fetched from <c>periodni.com</c>.
        /// </summary>
        /// <returns></returns>
        public static List<Element> GetElements()
        {
            return _cachedElements;
        }
    }
}
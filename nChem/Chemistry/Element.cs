using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace nChem.Chemistry
{
    /// <summary>
    /// Represents a chemical element.
    /// </summary>
    public sealed partial class Element
    {
        #region Elements

        [JsonIgnore]
        public static Element
            Hydrogen = Create(1),
            Helium = Create(2),
            Lithium = Create(3),
            Beryllium = Create(4),
            Boron = Create(5),
            Carbon = Create(6),
            Nitrogen = Create(7),
            Oxygen = Create(8),
            Fluorine = Create(9),
            Neon = Create(10),
            Sodium = Create(11),
            Magnesium = Create(12),
            Aluminium = Create(13),
            Silicon = Create(14),
            Phosphorus = Create(15),
            Sulfur = Create(16),
            Chlorine = Create(17),
            Argon = Create(18),
            Potassium = Create(19),
            Calcium = Create(20),
            Scandium = Create(21),
            Titanium = Create(22),
            Vanadium = Create(23),
            Chromium = Create(24),
            Manganese = Create(25),
            Iron = Create(26),
            Cobalt = Create(27),
            Nickel = Create(28),
            Copper = Create(29),
            Zinc = Create(30),
            Gallium = Create(31),
            Germanium = Create(32),
            Arsenic = Create(33),
            Selenium = Create(34),
            Bromine = Create(35),
            Krypton = Create(36),
            Rubidium = Create(37),
            Strontium = Create(38),
            Yttrium = Create(39),
            Zirconium = Create(40),
            Niobium = Create(41),
            Molybdenum = Create(42),
            Technetium = Create(43),
            Ruthenium = Create(44),
            Rhodium = Create(45),
            Palladium = Create(46),
            Silver = Create(47),
            Cadmium = Create(48),
            Indium = Create(49),
            Tin = Create(50),
            Antimony = Create(51),
            Tellurium = Create(52),
            Iodine = Create(53),
            Xenon = Create(54),
            Caesium = Create(55),
            Barium = Create(56),
            Lanthanum = Create(57),
            Cerium = Create(58),
            Praseodymium = Create(59),
            Neodymium = Create(60),
            Promethium = Create(61),
            Samarium = Create(62),
            Europium = Create(63),
            Gadolinium = Create(64),
            Terbium = Create(65),
            Dysprosium = Create(66),
            Holmium = Create(67),
            Erbium = Create(68),
            Thulium = Create(69),
            Ytterbium = Create(70),
            Lutetium = Create(71),
            Hafnium = Create(72),
            Tantalum = Create(73),
            Tungsten = Create(74),
            Rhenium = Create(75),
            Osmium = Create(76),
            Iridium = Create(77),
            Platinum = Create(78),
            Gold = Create(79),
            Mercury = Create(80),
            Thallium = Create(81),
            Lead = Create(82),
            Bismuth = Create(83),
            Polonium = Create(84),
            Astatine = Create(85),
            Radon = Create(86)
            ;

        #endregion

        /// <summary>
        /// Initializes an instance of the <see cref="Element"/> class.
        /// </summary>
        /// <param name="name">The element name.</param>
        /// <param name="symbol">The element symbol.</param>
        /// <param name="number">The element atomic number.</param>
        /// <param name="group">The periodic element group.</param>
        /// <param name="period">The element period.</param>
        /// <param name="category">The element category.</param>
        internal Element(string name, string symbol, int number, int group, int period, string category)
        {
            Name = name;
            Symbol = symbol;
            AtomicNumber = number;
            Group = group;
            Period = period;
            Category = category;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="Element"/> class using a specific atomic number.
        /// </summary>
        /// <param name="atomicNumber">The atomic number of the element.</param>
        public Element(int atomicNumber)
        {
            AtomicNumber = atomicNumber;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="Element"/> class.
        /// </summary>
        public Element()
        { }

        #region Properties

        /// <summary>
        /// Gets a value indicating if the <see cref="Element"/> is a metal.
        /// </summary>
        public bool IsMetal => FormatUtils.Metals.Contains(Category);

        /// <summary>
        /// Gets the mass number of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("massNumber")]
        public int MassNumber => (int) Math.Round(AtomicWeight ?? default(float));

        /// <summary>
        /// Gets the periodic group of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("group")]
        public int Group { get; set; }

        /// <summary>
        /// Gets the block of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("block", NullValueHandling = NullValueHandling.Ignore)]
        public string Block { get; set; }

        /// <summary>
        /// Gets the period of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("period")]
        public int Period { get; set; }

        /// <summary>
        /// Gets the category of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public string Category { get; set; }

        /// <summary>
        /// Gets the name of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets the symbol of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// Gets the atomic number of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("atomicNumber")]
        public int AtomicNumber { get; set; }

        /// <summary>
        /// Gets the atomic weight of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("atomicWeight")]
        public float? AtomicWeight { get; set; }

        /// <summary>
        /// Gets the electron configuration of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("electronConfiguration", NullValueHandling = NullValueHandling.Ignore)]
        public string ElectronConfiguration { get; set; }

        /// <summary>
        /// Gets the melting point of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("meltingPoint")]
        public float MeltingPoint { get; set; }

        /// <summary>
        /// Gets the boiling point of the <see cref="Element"/>-
        /// </summary>
        [JsonProperty("boilingPoint")]
        public float BoilingPoint { get; set; }

        /// <summary>
        /// Gets the density of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("density")]
        public float Density { get; set; }

        /// <summary>
        /// Gets the density of the liquid form of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("liquidDensity")]
        public float LiquidDensity { get; set; }

        /// <summary>
        /// Gets the heat of fusion of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("heatOfFusion")]
        public float HeatOfFusion { get; set; }

        /// <summary>
        /// Gets the heat of vaporization of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("heatOfVaporization")]
        public float HeatOfVaporization { get; set; }

        /// <summary>
        /// Gets the molar heat capacity of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("molarHeatCapacity")]
        public float MolarHeatCapacity { get; set; }

        /// <summary>
        /// Gets the electronegativity of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("electronegativity")]
        public float Electronegativity { get; set; }

        /// <summary>
        /// Gets or sets the atomic radius of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("atomicRadius")]
        public float AtomicRadius { get; set; }

        /// <summary>
        /// Gets or sets the thermal conductivity of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("thermalConductivity")]
        public string ThermalConductivity { get; set; }

        /// <summary>
        /// Gets or sets the heat of atomization of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("heatOfAtomization")]
        public float HeatOfAtomization { get; set; }

        /// <summary>
        /// Gets or sets the first ionization energy of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("firstIonizationEnergy")]
        public float FirstIonizationEnergy { get; set; }

        /// <summary>
        /// Gets or sets the second ionization energy of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("secondIonizationEnergy")]
        public float SecondIonizationEnergy { get; set; }

        /// <summary>
        /// Gets or sets the third ionization energy of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("thirdIonizationEnergy")]
        public float ThirdIonizationEnergy { get; set; }

        /// <summary>
        /// Gets or sets the abundance in the atmosphere of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("abundanceAtmosphere")]
        public string AbundanceInAtmosphere { get; set; }

        /// <summary>
        /// Gets or sets the abundance in the oceans of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("abundanceOceans")]
        public string AbundanceInOceans { get; set; }

        /// <summary>
        /// Gets or sets the abundance in the earth's crust of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("abundanceEarth")]
        public float AbundanceInEarth { get; set; }

        /// <summary>
        /// Gets or sets the crystal structure of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("crystalStructure")]
        public string CrystalStructure { get; set; }

        /// <summary>
        /// Gets or sets the unit-cell dimensions of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("unitCellDimensions")]
        public string UnitCellDimensions { get; set; }

        /// <summary>
        /// Gets or sets the space group of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("spaceGroup")]
        public string SpaceGroup { get; set; }

        /// <summary>
        /// Gets or sets the molar volume of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("molarVolume")]
        public float MolarVolume { get; set; }

        /// <summary>
        /// Gets the amount of protons in the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("protons")]
        public int Protons => AtomicNumber;

        /// <summary>
        /// Gets the amount of electrons in the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("electrons")]
        public int Electrons => AtomicNumber;

        /// <summary>
        /// Gets the amount of neutrons in the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("neutrons")]
        public int Neutrons => MassNumber - AtomicNumber;

        #endregion

        #region Operator overloads

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            return (obj as Element)?.AtomicNumber == AtomicNumber;
        }

        /// <summary>Serves as the default hash function. </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return AtomicNumber;
        }

        /// <summary>
        /// Adds the left-hand operand with the right-hand operand.
        /// </summary>
        /// <param name="e1">The left-hand operand.</param>
        /// <param name="e2">The right-hand operand.</param>
        /// <returns></returns>
        public static Element operator +(Element e1, Element e2)
        {
            return Element.Create(e1.AtomicNumber + e2.AtomicNumber);
        }

        /// <summary>
        /// Subtracts the left-hand operand with the right-hand operand.
        /// </summary>
        /// <param name="e1">The left-hand operand.</param>
        /// <param name="e2">The right-hand operand.</param>
        /// <returns></returns>
        public static Element operator -(Element e1, Element e2)
        {
            return Element.Create(e1.AtomicNumber - e2.AtomicNumber);
        }

        #endregion

        public static Element Create(int atomicNumber)
        {
            if (Element.GetElements() == null || Element.GetElements().Count == 0)
            {
                Element.FetchPeriodni();
                File.WriteAllText("elements.json", JsonConvert.SerializeObject(Element.GetElements(), Formatting.Indented));
            }

            Element result = new Element(atomicNumber);
            Element element = Element.GetElements()?
                .FirstOrDefault(x => x.AtomicNumber == atomicNumber);

            if (element == null)
                throw new NullReferenceException();

            result.Group = element.Group;
            result.Block = element.Block;
            result.Period = element.Period;
            result.Category = element.Category;
            result.Name = element.Name;
            result.Symbol = element.Symbol;
            result.AtomicWeight = element.AtomicWeight;
            result.ElectronConfiguration = element.ElectronConfiguration;
            result.MeltingPoint = element.MeltingPoint;
            result.BoilingPoint = element.BoilingPoint;
            result.Density = element.Density;
            result.LiquidDensity = element.LiquidDensity;
            result.HeatOfVaporization = element.HeatOfVaporization;
            result.HeatOfFusion = element.HeatOfFusion;
            result.HeatOfAtomization = element.HeatOfAtomization;
            result.FirstIonizationEnergy = element.FirstIonizationEnergy;
            result.SecondIonizationEnergy = element.SecondIonizationEnergy;
            result.ThirdIonizationEnergy = element.ThirdIonizationEnergy;
            result.AtomicRadius = element.AtomicRadius;
            result.MolarHeatCapacity = element.MolarHeatCapacity;
            result.AbundanceInAtmosphere = element.AbundanceInAtmosphere;
            result.AbundanceInEarth = element.AbundanceInEarth;
            result.AbundanceInOceans = element.AbundanceInOceans;
            result.CrystalStructure = element.CrystalStructure;
            result.SpaceGroup = element.SpaceGroup;
            result.MolarVolume = element.MolarVolume;
            result.UnitCellDimensions = element.UnitCellDimensions;
            result.Electronegativity = element.Electronegativity;

            return result;
        }

        /// <summary>
        /// Returns the right neighbor of the current <see cref="Element"/> instance.
        /// </summary>
        /// <returns></returns>
        public Element GetRightNeighbor()
        {
            if (AtomicNumber + 1 > Element.GetMaxAtomicNumber())
                throw new IndexOutOfRangeException();

            return Element.Create(AtomicNumber + 1);
        }

        /// <summary>
        /// Returns the left neighbor of the current <see cref="Element"/> instance.
        /// </summary>
        /// <returns></returns>
        public Element GetLeftNeighbor()
        {
            if (AtomicNumber - 1 < 1)
                throw new IndexOutOfRangeException();

            return Element.Create(AtomicNumber - 1);
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Symbol} ({Name})";
        }
    }
}

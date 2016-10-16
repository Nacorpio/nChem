using System;
using System.Linq;
using Newtonsoft.Json;

namespace nChem
{
    /// <summary>
    /// Represents a chemical element.
    /// </summary>
    public sealed partial class Element
    {
        [JsonIgnore]
        public static Element
            Lithium = new Element(3),
            Iridium = new Element(77),
            Tungsten = new Element(74),
            Hydrogen = new Element(1),
            Helium = new Element(2),
            Beryllium = new Element(4),
            Boron = new Element(5),
            Carbon = new Element(6),
            Nitrogen = new Element(7),
            Oxygen = new Element(8),
            Fluorine = new Element(9),
            Neon = new Element(10),
            Sodium = new Element(11),
            Magnesium = new Element(12),
            Aluminium = new Element(13),
            Silicon = new Element(14),
            Phosphorus = new Element(15),
            Sulfur = new Element(16),
            Chlorine = new Element(17),
            Argon = new Element(18),
            Potassium = new Element(19),
            Calcium = new Element(20),
            Scandium = new Element(21),
            Titanium = new Element(22),
            Vanadium = new Element(23),
            Chromium = new Element(24),
            Manganese = new Element(25),
            Iron = new Element(26),
            Cobalt = new Element(27),
            Nickel = new Element(28),
            Copper = new Element(29),
            Zinc = new Element(30),
            Gallium = new Element(31),
            Germanium = new Element(32),
            Arsenic = new Element(33),
            Selenium = new Element(34),
            Bromine = new Element(35),
            Krypton = new Element(36),
            Rubidium = new Element(37),
            Strontium = new Element(38),
            Yttrium = new Element(39),
            Zirconium = new Element(40),
            Niobium = new Element(41),
            Molybdenum = new Element(42),
            Technetium = new Element(43),
            Ruthenium = new Element(44),
            Rhodium = new Element(45),
            Palladium = new Element(46),
            Silver = new Element(47),
            Cadmium = new Element(48),
            Indium = new Element(49),
            Tin = new Element(50),
            Antimony = new Element(51),
            Tellurium = new Element(52),
            Iodine = new Element(53),
            Xenon = new Element(54)
            ;

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
            if (GetElements() == null || GetElements().Count == 0)
                FetchPeriodni();

            Element element = GetElements()?.FirstOrDefault(x => x.AtomicNumber == atomicNumber);
            if (element == null)
                return;

            Group = element.Group;
            Block = element.Block;
            Period = element.Period;
            Category = element.Category;
            Name = element.Name;
            Symbol = element.Symbol;
            AtomicNumber = element.AtomicNumber;
            AtomicWeight = element.AtomicWeight;
            ElectronConfiguration = element.ElectronConfiguration;
            MeltingPoint = element.MeltingPoint;
            BoilingPoint = element.BoilingPoint;
            Density = element.Density;
            LiquidDensity = element.LiquidDensity;
            HeatOfVaporization = element.HeatOfVaporization;
            HeatOfFusion = element.HeatOfFusion;
            HeatOfAtomization = element.HeatOfAtomization;
            FirstIonizationEnergy = element.FirstIonizationEnergy;
            SecondIonizationEnergy = element.SecondIonizationEnergy;
            ThirdIonizationEnergy = element.ThirdIonizationEnergy;
            AtomicRadius = element.AtomicRadius;
            MolarHeatCapacity = element.MolarHeatCapacity;
            AbundanceInAtmosphere = element.AbundanceInAtmosphere;
            AbundanceInEarth = element.AbundanceInEarth;
            AbundanceInOceans = element.AbundanceInOceans;
            CrystalStructure = element.CrystalStructure;
            SpaceGroup = element.SpaceGroup;
            MolarVolume = element.MolarVolume;
            UnitCellDimensions = element.UnitCellDimensions;
            Electronegativity = element.Electronegativity;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="Element"/> class.
        /// </summary>
        public Element()
        { }

        #region Properties

        /// <summary>
        /// Gets the mass number of the <see cref="Element"/>.
        /// </summary>
        [JsonProperty("massNumber")]
        public int MassNumber => (int) Math.Round(AtomicWeight);

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
        public float AtomicWeight { get; set; }

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

        /// <summary>
        /// Determines whether the left-hand side is equal to the right-hand side.
        /// </summary>
        /// <param name="e1">The left-hand operand.</param>
        /// <param name="e2">The right-hand operand.</param>
        /// <returns></returns>
        public static bool operator ==(Element e1, Element e2)
        {
            return e1?.AtomicNumber == e2?.AtomicNumber;
        }

        /// <summary>
        /// Determines whether the left-hand side is not equal to the right-hand side.
        /// </summary>
        /// <param name="e1">The left-hand operand.</param>
        /// <param name="e2">The right-hand operand.</param>
        /// <returns></returns>
        public static bool operator !=(Element e1, Element e2)
        {
            return !(e1 == e2);
        }

        /// <summary>
        /// Adds the left-hand operand with the right-hand operand.
        /// </summary>
        /// <param name="e1">The left-hand operand.</param>
        /// <param name="e2">The right-hand operand.</param>
        /// <returns></returns>
        public static Element operator +(Element e1, Element e2)
        {
            return new Element(e1.AtomicNumber + e2.AtomicNumber);
        }

        /// <summary>
        /// Subtracts the left-hand operand with the right-hand operand.
        /// </summary>
        /// <param name="e1">The left-hand operand.</param>
        /// <param name="e2">The right-hand operand.</param>
        /// <returns></returns>
        public static Element operator -(Element e1, Element e2)
        {
            return new Element(e1.AtomicNumber - e2.AtomicNumber);
        }

        #endregion

        /// <summary>
        /// Returns the right neighbour of the current <see cref="Element"/> instance.
        /// </summary>
        /// <returns></returns>
        public Element GetRightNeighbour()
        {
            if (AtomicNumber + 1 > GetMaxAtomicNumber())
                throw new IndexOutOfRangeException();

            return new Element(AtomicNumber + 1);
        }

        /// <summary>
        /// Returns the left neighbour of the current <see cref="Element"/> instance.
        /// </summary>
        /// <returns></returns>
        public Element GetLeftNeighbour()
        {
            if (AtomicNumber - 1 < 1)
                throw new IndexOutOfRangeException();

            return new Element(AtomicNumber - 1);
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Symbol} ({Name})";
        }
    }
}

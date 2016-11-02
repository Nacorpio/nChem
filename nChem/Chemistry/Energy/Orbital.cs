using nChem.Chemistry.Particles;

namespace nChem.Chemistry.Energy
{
    /// <summary>
    /// Represents an orbit/path with a maximum amount of two electrons.
    /// </summary>
    public sealed class Orbital
    {
        private readonly Electron[] _electrons;

        /// <summary>
        /// Initializes an instance of the <see cref="Orbital"/> class.
        /// </summary>
        public Orbital(int value)
        {
            _electrons = new Electron[2];
            Value = value;
        }

        /// <summary>
        /// Gets the value of the <see cref="Orbital"/>.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Gets or sets an electron at the specified zero-based index.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns></returns>
        public Electron this[int index]
        {
            get { return _electrons[index]; }
            set { _electrons[index] = value; }
        }

        /// <summary>
        /// Returns the electrons of the <see cref="Orbital"/>.
        /// </summary>
        /// <returns></returns>
        public Electron[] GetElectrons()
        {
            return _electrons;
        }

        /// <summary>
        /// Determines whether the <see cref="Orbital"/> is empty.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return this[0] == null && this[1] == null;
        }

        /// <summary>
        /// Determines whether the <see cref="Orbital"/> is paired.
        /// </summary>
        /// <returns></returns>
        public bool IsPaired()
        {
            if (this[0] == null && this[1] == null)
                return true;

            if (this[0] == null || this[1] == null)
                return false;

            return this[0].Spin == ElectronSpin.Up && this[1].Spin == ElectronSpin.Down;
        }

        /// <summary>
        /// Fills the <see cref="Orbital"/> with two electrons.
        /// </summary>
        public void Fill()
        {
            Populate();
            Populate();
        }

        /// <summary>
        /// Populates the <see cref="Orbital"/> with an electron.
        /// </summary>
        public void Populate()
        {
            if (this[0] == null)
            {
                this[0] = new Electron(ElectronSpin.Up);
                return;
            }

            this[1] = new Electron(ElectronSpin.Down);
        }
    }
}
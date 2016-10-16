namespace nChem
{
    public sealed class Electron
    {
        /// <summary>
        /// Initializes an instance of the <see cref="Electron"/> class.
        /// </summary>
        /// <param name="spin"></param>
        public Electron(ElectronSpin spin)
        {
            Spin = spin;
        }

        /// <summary>
        /// Gets the spin of the <see cref="Electron"/>.
        /// </summary>
        public ElectronSpin Spin { get; }
    }
}
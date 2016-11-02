namespace nChem.Chemistry.Particles
{
    /// <summary>
    /// Represents an electron particle, which has a negative charge.
    /// </summary>
    public sealed class Electron : ICharged
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

        /// <summary>
        /// Returns the charge of the <see cref="Electron"/>.
        /// </summary>
        /// <returns></returns>
        public int GetCharge() => -1;
    }
}
namespace nChem.Chemistry.Particles
{
    /// <summary>
    /// Represents a proton particle, which has a positive charge.
    /// </summary>
    public sealed class Proton : ICharged
    {
        /// <summary>
        /// Returns the charge of the <see cref="Proton"/>.
        /// </summary>
        /// <returns></returns>
        public int GetCharge() => 1;
    }
}
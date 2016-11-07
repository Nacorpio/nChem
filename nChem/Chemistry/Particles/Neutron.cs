namespace nChem.Chemistry.Particles
{
    /// <summary>
    /// Represents an uncharged particle.
    /// </summary>
    public sealed class Neutron : ICharged
    {
        /// <summary>
        /// Returns the charge of the <see cref="Neutron"/>.
        /// </summary>
        /// <returns></returns>
        public int GetCharge()
        {
            return 0;
        }
    }
}

using System.Collections.Generic;

namespace nChem
{
    internal interface IChild<out T>
    {
        /// <summary>
        /// Gets the parent of the <see cref="IChild{T}"/> implementation.
        /// </summary>
        T Parent { get; }
    }

    /// <summary>
    /// Represents the implementation of an atomic atom or compound with a charge.
    /// </summary>
    public interface IAtomic
    {
        /// <summary>
        /// Returns the atomic weight of the <see cref="IAtomic"/> implementation.
        /// </summary>
        /// <returns></returns>
        float? GetAtomicWeight();

        /// <summary>
        /// Gets the electron amount of the <see cref="IAtomic"/> implementation.
        /// </summary>
        int Electrons { get; }

        /// <summary>
        /// Gets the proton amount of the <see cref="IAtomic"/> implementation.
        /// </summary>
        int Protons { get; }

        /// <summary>
        /// Gets the neutron amount of the <see cref="IAtomic"/> implementation.
        /// </summary>
        int Neutrons { get; }

        /// <summary>
        /// Determines whether the <see cref="IAtomic"/> implementation is a compound.
        /// </summary>
        /// <returns></returns>
        bool IsCompound();

        /// <summary>
        /// Returns the elements in the <see cref="IAtomic"/> implementation.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Chemistry.Element> GetElements();
    }

    /// <summary>
    /// Represents the implementation of an atom/molecule with a charge (ion).
    /// </summary>
    public interface IChargeable
    {
        /// <summary>
        /// Gets the charge of the <see cref="IChargeable"/> implementation.
        /// </summary>
        /// <returns></returns>
        int Charge { get; }
    }
}

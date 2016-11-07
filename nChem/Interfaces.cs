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
        float? AtomicWeight { get; }

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
        /// Converts the current <see cref="IAtomic"/> implement to an <see cref="Chemistry.Ion"/> instance.
        /// </summary>
        /// <returns></returns>
        Chemistry.Ion ToIon();

        /// <summary>
        /// Returns whether the <see cref="IAtomic"/> implementation is an <see cref="Chemistry.Ion"/>.
        /// </summary>
        bool IsIon();

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
    public interface ICharged
    {
        /// <summary>
        /// Returns the charge of the <see cref="ICharged"/> implementation.
        /// </summary>
        /// <returns></returns>
        int GetCharge();
    }
}

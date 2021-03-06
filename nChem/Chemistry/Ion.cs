﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace nChem.Chemistry
{
    /// <summary>
    /// Represents an <see cref="Atom"/> with a non-neutral charge.
    /// </summary>
    public sealed class Ion : ICharged
    {
        /// <summary>
        /// Initializes an instance of the <see cref="Ion"/> class.
        /// </summary>
        /// <param name="content">The atomic content.</param>
        public Ion(IAtomic content)
        {
            Content = content;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="Ion"/> class.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="charge">The charge.</param>
        public Ion(Element element, int charge = 0)
            : this(new Atom(element, charge))
        { }

        /// <summary>
        /// Initializes an instance of the <see cref="Ion"/> class.
        /// </summary>
        /// <param name="atoms">The atoms.</param>
        public Ion(IEnumerable<Atom> atoms)
            : this(new Compound(atoms.Select(x => new Stack(x))))
        { }

        /// <summary>
        /// Gets the content of the <see cref="Ion"/>.
        /// </summary>
        public IAtomic Content { get; }

        /// <summary>
        /// Gets a value indicating if the <see cref="Ion"/> is polyatomic (more than one atom).
        /// </summary>
        /// <returns></returns>
        public bool IsPolyatomic => Content.IsCompound() && ((Compound)Content).Sum(x => x.Size) > 1;

        /// <summary>
        /// Gets a value indicating if the <see cref="Ion"/> is a cation (has a positive charge).
        /// </summary>
        /// <returns></returns>
        public bool IsCation => GetCharge() > 0;

        /// <summary>
        /// Gets a value indicating if the <see cref="Ion"/> is an anion (has a negative charge).
        /// </summary>
        /// <returns></returns>
        public bool IsAnion => GetCharge() < 0;

        /// <summary>
        /// Gets a value indicating if the <see cref="Ion"/> is monatomic (only one element).
        /// </summary>
        /// <returns></returns>
        public bool IsMonatomic => !Content.IsCompound();

        /// <summary>
        /// Returns the charge of the <see cref="Ion"/>.
        /// </summary>
        /// <returns></returns>
        public int GetCharge() => -(Content.Electrons - Content.Protons);

        /// <summary>
        /// Converts a specific <see cref="Compound"/> to an ion.
        /// </summary>
        /// <param name="compound">The compound to convert.</param>
        public static implicit operator Ion(Compound compound)
        {
            if (!compound.IsIon()) 
                throw new ArgumentException("The specified compound isn't an ion.", nameof(compound));

            return new Ion(compound);
        }

        /// <summary>
        /// Converts a specific <see cref="Atom"/> to an ion.
        /// </summary>
        /// <param name="atom">The atom to convert.</param>
        public static implicit operator Ion(Atom atom)
        {
            if (!atom.IsIon())
                throw new ArgumentException("The specified atom isn't an ion.", nameof(atom));

            return new Ion(atom);
        }

        /// <summary>
        /// Converts the current <see cref="Ion"/> instance to a <see cref="Compound"/>.
        /// </summary>
        /// <returns></returns>
        public Compound ToCompound()
        {
            return Content.IsCompound() ? (Compound) Content : null;
        }

        /// <summary>
        /// Converts the current <see cref="Ion"/> instance to an <see cref="Atom"/>.
        /// </summary>
        /// <returns></returns>
        public Atom ToAtom()
        {
            return !Content.IsCompound() ? (Atom) Content : null;
        }

        /// <summary>
        /// Determines whether the <see cref="Ion"/> contains a specific element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        internal bool Contains(Element element)
        {
            return Content.GetElements().Any(x => Equals(x, element));
        }

        /// <summary>
        /// Determines whether the <see cref="ICharged"/> implementation is charged.
        /// </summary>
        /// <returns></returns>
        public bool IsCharged()
        {
            return true;
        }
    }
}
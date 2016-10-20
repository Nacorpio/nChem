namespace nChem
{
    public interface IChild<out T>
    {
        /// <summary>
        /// Gets the parent of the <see cref="IChild{T}"/> implementation.
        /// </summary>
        T Parent { get; }
    }
}

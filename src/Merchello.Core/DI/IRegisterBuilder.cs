namespace Merchello.Core.DI
{
    /// <summary>
    /// Represents a collection register.
    /// </summary>
    /// <typeparam name="TRegister">The type of the collection.</typeparam>
    /// <typeparam name="TItem">The type of the items.</typeparam>
    public interface IRegisterBuilder<out TRegister, TItem>
        where TRegister : IRegister<TItem>
    {
        /// <summary>
        /// Creates a register.
        /// </summary>
        /// <returns>A register.</returns>
        /// <remarks>Creates a new collection each time it is invoked.</remarks>
        TRegister CreateRegister();
    }
}
namespace Merchello.Core.Services
{
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a data service for <see cref="IStore"/>.
    /// </summary>
    public interface IStoreService : IGetAllService<IStore>
    {
        /// <summary>
        /// Create a <see cref="IStore"/> without saving it to the database.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="alias">
        /// The alias.
        /// </param>
        /// <returns>
        /// The <see cref="IStore"/>.
        /// </returns>
        IStore Create(string name, string alias);

        /// <summary>
        /// Create a <see cref="IStore"/> and saves it to the database.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="alias">
        /// The alias.
        /// </param>
        /// <returns>
        /// The <see cref="IStore"/>.
        /// </returns>
        IStore CreateWithKey(string name, string alias);

        /// <summary>
        /// Gets a store by it's unique store alias.
        /// </summary>
        /// <param name="alias">
        /// The alias.
        /// </param>
        /// <returns>
        /// The <see cref="IStore"/>.
        /// </returns>
        IStore GetByAlias(string alias);
    }
}
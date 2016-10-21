namespace Merchello.Core.Persistence.Querying
{
    using Merchello.Core.Persistence.Mappers;

    /// <summary>
    /// Represents a factory responsible for translating entity queries.
    /// </summary>
    public interface IQueryFactory : IExposeSqlSyntax
    {
        /// <summary>
        /// Gets the MapperRegister for mapping properties between entities and DTO (POCO) classes.
        /// </summary>
        IMapperRegister Mappers { get; }

        /// <summary>
        /// Creates the query.
        /// </summary>
        /// <typeparam name="T">
        /// The type of entity to be queried
        /// </typeparam>
        /// <returns>
        /// The <see cref="IQuery{T}"/>.
        /// </returns>
        IQuery<T> Create<T>();
    }
}
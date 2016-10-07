namespace Merchello.Core.Persistence.UnitOfWork
{
    /// <summary>
    /// Represents a base NPoco unit of work provider
    /// </summary>
    internal abstract class NPocoUnitOfWorkProviderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NPocoUnitOfWorkProviderBase"/> class with a database factory and a repository factory.
        /// </summary>
        /// <param name="databaseFactory">A database factory.</param>
        /// <param name="repositoryFactory">A repository factory.</param>
        protected NPocoUnitOfWorkProviderBase(IDatabaseFactory databaseFactory, RepositoryFactory repositoryFactory)
        {
            Ensure.ParameterNotNull(databaseFactory, nameof(databaseFactory));
            Ensure.ParameterNotNull(repositoryFactory, nameof(repositoryFactory));
            this.DatabaseFactory = databaseFactory;
            this.RepositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Gets the database factory.
        /// </summary>
        protected IDatabaseFactory DatabaseFactory { get; }

        /// <summary>
        /// Gets the repository factory.
        /// </summary>
        protected RepositoryFactory RepositoryFactory { get; }
    }
}
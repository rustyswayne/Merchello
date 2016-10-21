namespace Merchello.Tests.Base.Fake
{
    using global::Umbraco.Core.Persistence.Mappers;
    using global::Umbraco.Core.Persistence.Querying;
    using global::Umbraco.Core.Persistence.SqlSyntax;

    using Moq;

    public class FakeQueryFactory : IQueryFactory
    {

        public FakeQueryFactory(ISqlSyntaxProvider sqlSyntax)
        {
            this.Mappers = new Mock<IMapperCollection>().Object;

            this.SqlSyntax = sqlSyntax;
        }

        public IQuery<T> Create<T>()
        {
            throw new System.NotImplementedException();
        }

        public IMapperCollection Mappers { get; }

        public ISqlSyntaxProvider SqlSyntax { get; }
    }
}
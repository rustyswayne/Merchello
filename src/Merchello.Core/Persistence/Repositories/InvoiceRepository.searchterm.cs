namespace Merchello.Core.Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    using NPoco;

    /// <inheritdoc/>
    internal partial class InvoiceRepository : ISearchTermRepository<IInvoice>
    {

        /// <summary>
        /// The valid sort fields.
        /// </summary>
        private static readonly string[] ValidSortFields = { "invoicenumber", "invoicedate", "billtoname", "billtoemail" };

        /// <inheritdoc/>
        public PagedCollection<IInvoice> SearchForTerm(string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var sql = BuildSearchSql(searchTerm).AppendOrderExpression<InvoiceDto>(ValidateSortByField(orderExpression), direction);
            return Database.Page<InvoiceDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <summary>
        /// Builds customer search SQL.
        /// </summary>
        /// <param name="searchTerm">
        /// The search term.
        /// </param>
        /// <returns>
        /// The <see cref="Sql{TContext}"/>.
        /// </returns>
        /// REFACTOR
        private Sql<SqlContext> BuildSearchSql(string searchTerm)
        {
            searchTerm = searchTerm.Replace(",", " ");
            var invidualTerms = searchTerm.Split(' ');

            var numbers = new List<int>();
            var terms = new List<string>();

            foreach (var term in invidualTerms.Where(x => !string.IsNullOrEmpty(x)))
            {
                int invoiceNumber;
                if (int.TryParse(term, out invoiceNumber))
                {
                    numbers.Add(invoiceNumber);
                }
                else
                {
                    terms.Add(term);
                }
            }


            var sql = GetBaseQuery(false);

            if (numbers.Any() && terms.Any())
            {
                sql.Where(
                    "billToName LIKE @term OR billToEmail LIKE @email OR billToAddress1 LIKE @adr1 OR billToLocality LIKE @loc OR invoiceNumber IN (@invNo) OR billToPostalCode IN (@postal)",
                    new
                    {
                        @term = string.Format("%{0}%", string.Join("% ", terms)).Trim(),
                        @email = string.Format("%{0}%", string.Join("% ", terms)).Trim(),
                        @adr1 = string.Format("%{0}%", string.Join("%", terms)).Trim(),
                        @loc = string.Format("%{0}%", string.Join("%", terms)).Trim(),
                        @invNo = numbers.ToArray(),
                        @postal = string.Format("%{0}%", string.Join("%", terms)).Trim()
                    });
            }
            else if (numbers.Any())
            {
                if (numbers.Count() == 1)
                {
                    int number = numbers[0];
                    // if there is only one number, use starts-with type logic so that the list descreases as more digits are inserted.
                    // invoiceNumber is indexed, so use index by including ranges. Query looks ugly, but more effectcient than casting
                    // invoiceNumber to a string and using a 'like' - which wouldn't use an index.
                    // postcode is a string and not indexed - so is doing a full table scan. If performance is an issue on large data sets,
                    //   consider removing postcode from lookup or enhancing with an index
                    sql.Where(
                        "invoiceNumber = @invNo OR invoiceNumber BETWEEN @invNo10 AND @invNo19 OR invoiceNumber BETWEEN @invNo100 AND @invNo199 OR invoiceNumber BETWEEN @invNo1000 AND @invNo1999 OR invoiceNumber BETWEEN @invNo10000 AND @invNo19999 OR invoiceNumber BETWEEN @invNo100000 AND @invNo199999 OR invoiceNumber BETWEEN @invNo1000000 AND @invNo1999999 OR billToPostalCode LIKE @postal ",
                        new
                        {
                            @invNo = number,
                            @invNo10 = number * 10,
                            @invNo19 = number * 10 + 9,
                            @invNo100 = number * 100,
                            @invNo199 = number * 100 + 99,
                            @invNo1000 = number * 1000,
                            @invNo1999 = number * 1000 + 999,
                            @invNo10000 = number * 10000,
                            @invNo19999 = number * 10000 + 9999,
                            @invNo100000 = number * 100000,
                            @invNo199999 = number * 100000 + 99999,
                            @invNo1000000 = number * 1000000,
                            @invNo1999999 = number * 1000000 + 999999,
                            @postal = string.Format("{0}%", number).Trim()
                        });
                }
                else
                {
                    sql.Where("invoiceNumber IN (@invNo) OR billToPostalCode IN (@postal) ", new { @invNo = numbers.ToArray(), @postal = numbers.ToArray() });
                }
            }
            else
            {
                sql.Where(
                    "billToName LIKE @term OR billToEmail LIKE @term OR billToAddress1 LIKE @adr1 OR billToLocality LIKE @loc OR billToPostalCode IN (@postal)",
                    new
                    {
                        @term = string.Format("%{0}%", string.Join("% ", terms)).Trim(),
                        @email = string.Format("%{0}%", string.Join("% ", terms)).Trim(),
                        @adr1 = string.Format("%{0}%", string.Join("%", terms)).Trim(),
                        @loc = string.Format("%{0}%", string.Join("%", terms)).Trim(),
                        @postal = string.Format("%{0}%", string.Join("%", terms)).Trim()
                    });
            }

            return sql;
        }

        /// <summary>
        /// Validates the sort by string is a valid sort by field
        /// </summary>
        /// <param name="sortBy">
        /// The sort by.
        /// </param>
        /// <returns>
        /// A validated database field name.
        /// </returns>
        private string ValidateSortByField(string sortBy)
        {
            return ValidSortFields.Contains(sortBy.ToLowerInvariant()) ? sortBy : "invoiceNumber";
        }
    }
}

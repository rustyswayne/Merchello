namespace Merchello.Core.Gateways.Taxation
{
    using Merchello.Core.Acquired;
    using Merchello.Core.Strategies;

    /// <summary>
    /// Defines a taxation strategy
    /// </summary>
    public interface ITaxCalculationStrategy : IStrategy
    {
        /// <summary>
        /// Computes the invoice tax result
        /// </summary>
        /// <returns>The <see cref="ITaxCalculationResult"/></returns>
        Attempt<ITaxCalculationResult> CalculateTaxesForInvoice();
    }
}

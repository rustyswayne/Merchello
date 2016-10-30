namespace Merchello.Core.Chains.InvoiceCreation
{
    using Merchello.Core.Models;

    /// <summary>
    /// The invoice builder task register.
    /// </summary>
    public class InvoiceBuilderTaskRegister : ConfigurationChainTaskRegister<IInvoice>
    {
        public InvoiceBuilderTaskRegister(string chainAlias)
            : base(chainAlias)
        {
        }

        protected override IAttemptChainTask<IInvoice> CreateInstance()
        {
            throw new System.NotImplementedException();
        }
    }
}
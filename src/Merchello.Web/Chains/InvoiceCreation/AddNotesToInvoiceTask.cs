namespace Merchello.Web.Chains.InvoiceCreation
{
    using System.Linq;

    using Merchello.Core;
    using Merchello.Core.Acquired;
    using Merchello.Core.Chains.InvoiceCreation;
    using Merchello.Core.Checkout;
    using Merchello.Core.Models;

    /// <summary>
    /// Adds any notes to the invoice.
    /// </summary>
    internal class AddNotesToInvoiceTask : CheckoutManagerInvoiceCreationAttemptChainTaskBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddNotesToInvoiceTask"/> class.
        /// </summary>
        /// <param name="checkoutManager">
        /// The <see cref="ICheckoutManagerBase"/>.
        /// </param>
        public AddNotesToInvoiceTask(ICheckoutManagerBase checkoutManager)
            : base(checkoutManager)
        {
        }

        /// <summary>
        /// Adds notes to an invoice
        /// </summary>
        /// <param name="value">
        /// The <see cref="IInvoice"/>
        /// </param>
        /// <returns>
        /// The <see cref="Attempt"/>.
        /// </returns>
        public override Attempt<IInvoice> PerformTask(IInvoice value)
        {
            var notes = this.CheckoutManager.Extended.GetNotes().ToArray();

            if (!notes.Any()) return Attempt<IInvoice>.Succeed(value);

            var notesList = notes.Select(msg => this.CheckoutManager.Context.Services.NoteService.CreateNote(value.Key, EntityType.Invoice, msg)).ToList();

            foreach (var note in notesList)
            {
                note.Author = value.BillToEmail;
                note.InternalOnly = false;
            }

            value.Notes = notesList;

            return Attempt<IInvoice>.Succeed(value);
        }
    }
}
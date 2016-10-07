namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Linq;

    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Factories;

    /// <inheritdoc/>
    internal partial class InvoiceRepository : IInvoiceRepository
    {
        /// <summary>
        /// Saves the notes.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        private void SaveNotes(IInvoice entity)
        {
            var existing = _noteRepository.GetNotes(entity.Key);

            var removers = existing.Where(x => !Guid.Empty.Equals(x.Key) && entity.Notes.All(y => y.Key != x.Key)).ToArray();

            foreach (var remover in removers) _noteRepository.Delete(remover);

            var updates = entity.Notes.Where(x => removers.All(y => y.Key != x.Key));

            foreach (var u in updates)
            {
                u.EntityKey = entity.Key;
                _noteRepository.AddOrUpdate(u);
            }
        }
    }
}

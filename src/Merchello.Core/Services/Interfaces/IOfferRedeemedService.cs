namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a data service for <see cref="IOfferRedeemed"/>.
    /// </summary>
    public interface IOfferRedeemedService : IService
    {
        /// <summary>
        /// Gets an <see cref="IOfferRedeemed"/> record by it's key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="IOfferRedeemed"/>.
        /// </returns>
        IOfferRedeemed GetOfferRedeemedByKey(Guid key);

        /// <summary>
        ///  Gets a collection of <see cref="IOfferRedeemed"/> records by an invoice key.
        /// </summary>
        /// <param name="invoiceKey">
        /// The invoice key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IOfferRedeemed}"/>.
        /// </returns>
        IEnumerable<IOfferRedeemed> GetOfferRedeemedByInvoiceKey(Guid invoiceKey);

        /// <summary>
        ///  Gets a collection of <see cref="IOfferRedeemed"/> records by a customer key.
        /// </summary>
        /// <param name="customerKey">
        /// The customer key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IOfferRedeemed}"/>.
        /// </returns>
        IEnumerable<IOfferRedeemed> GetOfferRedeemedByCustomerKey(Guid customerKey);

        /// <summary>
        /// Gets a collection of <see cref="IOfferRedeemed"/> records by a offer settings key.
        /// </summary>
        /// <param name="offerSettingsKey">
        /// The offer settings key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IOfferRedeemed}"/>.
        /// </returns>
        IEnumerable<IOfferRedeemed> GetOfferRedeemedByOfferSettingsKey(Guid offerSettingsKey);

        /// <summary>
        /// The get by offer settings key and customer key.
        /// </summary>
        /// <param name="offerSettingsKey">
        /// The offer settings key.
        /// </param>
        /// <param name="customerKey">
        /// The customer key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IOfferRedeemed}"/>.
        /// </returns>
        IEnumerable<IOfferRedeemed> GetOfferRedeemedByOfferSettingsKeyAndCustomerKey(Guid offerSettingsKey, Guid customerKey);

        /// <summary>
        /// Gets a collection of <see cref="IOfferRedeemed"/> records by an offer provider key.
        /// </summary>
        /// <param name="offerProviderKey">
        /// The offer provider key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IOfferRedeemed}"/>.
        /// </returns>
        IEnumerable<IOfferRedeemed> GetOfferRedeemedByOfferProviderKey(Guid offerProviderKey);

        /// <summary>
        /// Gets the redemption count for an offer.
        /// </summary>
        /// <param name="offerSettingsKey">
        /// The offer settings key.
        /// </param>
        /// <returns>
        /// The current count of offer redemptions.
        /// </returns>
        int GetOfferRedeemedCount(Guid offerSettingsKey);

        /// <summary>
        /// Creates an <see cref="IOfferRedeemed"/> record
        /// </summary>
        /// <param name="offerSettings">
        /// The offer settings.
        /// </param>
        /// <param name="invoice">
        /// The invoice.
        /// </param>
        /// <returns>
        /// The <see cref="IOfferRedeemed"/>.
        /// </returns>
        IOfferRedeemed CreateOfferRedeemedWithKey(IOfferSettings offerSettings, IInvoice invoice);

        /// <summary>
        /// Saves an <see cref="IOfferRedeemed"/>
        /// </summary>
        /// <param name="redeemed">
        /// The offer redeemed.
        /// </param>
        void Save(IOfferRedeemed redeemed);

        /// <summary>
        /// Saves a collection of <see cref="IOfferRedeemed"/>
        /// </summary>
        /// <param name="redemptions">
        /// The redemptions.
        /// </param>
        void Save(IEnumerable<IOfferRedeemed> redemptions);

        /// <summary>
        /// Deletes an <see cref="IOfferRedeemed"/>
        /// </summary>
        /// <param name="redeemed">
        /// The offer redeemed.
        /// </param>
        void Delete(IOfferRedeemed redeemed);

        /// <summary>
        /// Deletes a collection of <see cref="IOfferRedeemed"/>.
        /// </summary>
        /// <param name="redemptions">
        /// The redemptions.
        /// </param>
        void Delete(IEnumerable<IOfferRedeemed> redemptions);
    }
}
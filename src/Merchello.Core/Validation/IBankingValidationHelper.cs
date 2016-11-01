namespace Merchello.Core.Validation
{
    /// <summary>
    /// Defines the BankingValidationHelper interface.
    /// </summary>
    public interface IBankingValidationHelper
    {
        /// <summary>
        /// Validate an International Bank Account Number (IBAN)
        /// </summary>
        /// <param name="iban">International Bank Account Number (IBAN) to validate</param>
        /// <returns>[true|false] whether IBAN is valid or not</returns>
        /// <seealso cref="http://en.wikipedia.org/wiki/International_Bank_Account_Number"/>
        /// <see cref="http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2"/>
        /// <seealso cref="http://en.wikipedia.org/wiki/ISO_7064"/>
        /// <seealso cref="http://www.tbg5-finance.org/?ibandocs.shtml"/> example.     
        bool IbanBanknrValid(string iban);
    }
}
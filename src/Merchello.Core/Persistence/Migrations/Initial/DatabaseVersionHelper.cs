namespace Merchello.Core.Persistence.Migrations.Initial
{
    using System;
    using System.Linq;

    using Merchello.Core.Configuration;

    /// <summary>
    /// Helper class to extract individual version checkouts outside of schema creation.
    /// </summary>
    internal static class DatabaseVersionHelper
    {
        /// <summary>
        /// The determine installed version.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <param name="success">
        /// A value indicating if the version could be established via checks.
        /// </param>
        /// <returns>
        /// The <see cref="Version"/>.
        /// </returns>
        public static Version DetermineInstalledVersion(DatabaseSchemaResult result, out bool success)
        {
            success = true;
            if (CheckTwoThreeOne(result)) return new Version(2, 3, 1);

            success = false;
            return MerchelloVersion.Current;
        }

        /// <summary>
        /// Checks if the version is 2.3.0.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool CheckTwoThreeOne(DatabaseSchemaResult result)
        {
            var v2DroppedTables = new[] { "merchInvoiceIndex", "merchCustomerIndex", "merchOrderIndex" };

            return result.Errors.Any(x => v2DroppedTables.Contains(x.Item2));
        }
    }
}
namespace Merchello.Core.Persistence.Migrations
{
    using System;

    /// <summary>
    /// Represents the Migration attribute, which is used to mark classes as
    /// database migrations with Up/Down methods for pushing changes UP or pulling them DOWN.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class MigrationAttribute : Attribute
    {
        public MigrationAttribute(string targetVersion, int sortOrder, string product)
        {
            this.TargetVersion = new Version(targetVersion);
            this.SortOrder = sortOrder;
            this.ProductName = product;
        }

        public MigrationAttribute(string minimumCurrentVersion, string targetVersion, int sortOrder, string product)
        {
            this.TargetVersion = new Version(targetVersion);
            this.MinimumCurrentVersion = new Version(minimumCurrentVersion);
            this.SortOrder = sortOrder;
            this.ProductName = product;
        }

        /// <summary>
        /// Gets the minimum current version for which this migration is allowed to execute
        /// </summary>
        public Version MinimumCurrentVersion { get; private set; }

        /// <summary>
        /// Gets the target version of this migration.
        /// </summary>
        public Version TargetVersion { get; private set; }

        /// <summary>
        /// Gets or sets the sort order, which is the order this migration will be run in.
        /// </summary>
        public int SortOrder { get; private set; }

        /// <summary>
        /// Gets or sets the name of the product, which this migration belongs to.
        /// </summary>
        public string ProductName { get; set; }
    }
}
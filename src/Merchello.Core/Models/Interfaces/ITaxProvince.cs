namespace Merchello.Core.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a province used in taxation.
    /// </summary>
    public interface ITaxProvince : IProvince
    {
        /// <summary>
        /// Gets or sets the percentage rate adjustment to the tax rate
        /// </summary>
        [DataMember]
        decimal PercentAdjustment { get; set; }
    }
}
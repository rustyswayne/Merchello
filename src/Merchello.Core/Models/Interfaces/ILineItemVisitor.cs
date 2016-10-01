namespace Merchello.Core.Models
{
    using Merchello.Core.Models.Interfaces;

    /// <summary>
    /// Represents a line item visitor.
    /// </summary>
    public interface ILineItemVisitor : IVisitor<ILineItem>
    {
    }
}
namespace Merchello.Core.Gateways
{
    /// <summary>
    /// Represents a method resource reference offered by a provider.
    /// </summary>
    public interface IGatewayResource
    {
        /// <summary>
        /// Gets the unique provider service code or 'alias' for the gateway method.
        /// </summary>
        string ServiceCode { get; }

        /// <summary>
        /// Gets the descriptive name of the gateway method
        /// </summary>
        string Name { get; }
    }
}
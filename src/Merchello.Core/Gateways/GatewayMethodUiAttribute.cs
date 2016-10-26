namespace Merchello.Core.Gateways
{
    using System;

    /// <summary>
    /// Represents an attribute to provide context to front end implementers during method selection.
    /// </summary>
    /// <remarks>
    /// Initial use case is for payment methods and credit card forms and notification methods for data entry.
    /// 
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class GatewayMethodUiAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayMethodUiAttribute"/> class.
        /// </summary>
        /// <param name="alias">
        /// The alias.
        /// </param>
        public GatewayMethodUiAttribute(string alias)
        {
            Ensure.ParameterNotNullOrEmpty(alias, "alias");

            Alias = alias;
        }

        /// <summary>
        /// Gets the alias.
        /// </summary>
        public string Alias { get; private set; }
    }
}
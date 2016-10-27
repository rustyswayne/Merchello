namespace Merchello.Core.Gateways
{
    using System;
    using System.Runtime.Serialization;

    /// <inheritdoc/>
    [Serializable]
    [DataContract(IsReference = true)]
    public class GatewayResource : IGatewayResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayResource"/> class.
        /// </summary>
        /// <param name="serviceCode">
        /// The service code.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        public GatewayResource(string serviceCode, string name)
        {
            Ensure.ParameterNotNullOrEmpty(serviceCode, "serviceCode");
            Ensure.ParameterNotNullOrEmpty(name, "name");

            ServiceCode = serviceCode;
            Name = name;
        }

        /// <inheritdoc/>
        [DataMember]
        public string ServiceCode { get; private set; }

        /// <inheritdoc/>
        [DataMember]
        public string Name { get; internal set; }
    }
}
namespace Merchello.Core.ValueConverters
{
    using System;

    using Merchello.Core.Models.DetachedContent;

    using Newtonsoft.Json.Linq;

    using Umbraco.Core.Models;
    using Umbraco.Core.Services;

    /// <summary>
    /// A converter to assist in saving detached property data correctly.
    /// </summary>
    internal class DetachedPublishedPropertyConverter
    {
        /// <summary>
        /// The singleton instance of the converter.
        /// </summary>
        private static DetachedPublishedPropertyConverter _instance;

        /// <summary>
        /// The <see cref="IDataTypeService"/>.
        /// </summary>
        private readonly IDataTypeService _dataTypeService;

        /// <summary>
        /// A value to indicate if the converter singleton is ready.
        /// </summary>
        /// <remarks>
        /// Can be removed eventually when Integration tests get refactored with an instantiated ApplicationContext but to
        /// </remarks>
        private readonly bool _ready;

        /// <summary>
        /// Initializes a new instance of the <see cref="DetachedPublishedPropertyConverter"/> class.
        /// </summary>
        /// <param name="dataTypeService">
        /// The data type service.
        /// </param>
        internal DetachedPublishedPropertyConverter(IDataTypeService dataTypeService)
        {
            this._dataTypeService = dataTypeService;

            // this is 
            this._ready = dataTypeService != null;
        }

        /// <summary>
        /// Gets a value indicating whether has current.
        /// </summary>
        public static bool HasCurrent
        {
            get
            {
                return _instance != null;
            }
        }

        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        public static DetachedPublishedPropertyConverter Current
        {
            get
            {
                return _instance;
            }

            internal set
            {
                _instance = value;
            }
        }

        public string ConvertDbToString(IContentType contentType, Property property)
        {
            if (!this._ready) return property.Value.ToString();

            throw new NotImplementedException();
        }

        public object ConvertDbToEditor(IDetachedContent detachedContent, Property property)
        {
            return JToken.FromObject(property.Value);
            throw new NotImplementedException();
        }
    }
}
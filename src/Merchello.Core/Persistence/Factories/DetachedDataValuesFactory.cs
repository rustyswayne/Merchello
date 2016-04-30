namespace Merchello.Core.Persistence.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Models.DetachedContent;
    using Merchello.Core.ValueConverters;

    using Newtonsoft.Json;

    using Umbraco.Core;
    using Umbraco.Core.Models;

    /// <summary>
    /// A factory for building converted property data values.
    /// </summary>
    internal class DetachedDataValuesFactory
    {
        /// <summary>
        /// The <see cref="DetachedPublishedPropertyValueConverter"/>.
        /// </summary>
        private readonly DetachedPublishedPropertyValueConverter _converter;

        /// <summary>
        /// The current <see cref="IContentType"/>.
        /// </summary>
        private IContentType _contentType; 

        /// <summary>
        /// Initializes a new instance of the <see cref="DetachedDataValuesFactory"/> class.
        /// </summary>
        /// <param name="detachedContentType">
        /// The <see cref="IDetachedContentType"/>.
        /// </param>
        /// <exception cref="NullReferenceException">
        /// Throws a null reference exception if the DetachedPublishedPropertyValueConverter has not been initialized
        /// </exception>
        public DetachedDataValuesFactory(IDetachedContentType detachedContentType)
        {
            if (!DetachedPublishedPropertyValueConverter.HasCurrent) throw new NullReferenceException("DetachedPublishedPropertyValueConverter singleton has not been set");
            _converter = DetachedPublishedPropertyValueConverter.Current;

            Initialize(detachedContentType);
        }

        /// <summary>
        /// Builds the property values from the database save values.
        /// </summary>
        /// <param name="dbJson">
        /// The database saved JSON.
        /// </param>
        /// <returns>
        /// The collection of property values.
        /// </returns>
        public IEnumerable<KeyValuePair<string, object>> Build(string dbJson)
        {
            if (dbJson.IsNullOrWhiteSpace()) return Enumerable.Empty<KeyValuePair<string, object>>();

            var dbValues = JsonConvert.DeserializeObject<IEnumerable<KeyValuePair<string, string>>>(dbJson).ToArray();

            return dbValues.Select(val => 
                new KeyValuePair<string, object>(val.Key, this._converter.ConvertDbToEditor(this._contentType, val)));
        }

        public string Build(IEnumerable<KeyValuePair<string, object>> editorValues)
        {
            var rawConverted =
                editorValues.Select(
                    val =>
                    new KeyValuePair<string, string>(val.Key, this._converter.ConvertEditorToDb(this._contentType, val)));

            return JsonConvert.SerializeObject(rawConverted);
        }

        /// <summary>
        /// Initializes the factory
        /// </summary>
        /// <param name="detachedContentType">
        /// The detached content type.
        /// </param>
        private void Initialize(IDetachedContentType detachedContentType)
        {
            _contentType = detachedContentType != null ?
                _converter.GetContentTypeFromDetachedContentType(detachedContentType) :
                null;
        }
    }
}
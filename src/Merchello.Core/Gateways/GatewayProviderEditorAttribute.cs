namespace Merchello.Core.Gateways
{
    using System;

    /// <summary>
    /// Represents an attribute that identifies a gateway provider and provides information for the back office.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class GatewayProviderEditorAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayProviderEditorAttribute"/> class.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="editorView">
        /// The editor view.
        /// </param>
        public GatewayProviderEditorAttribute(string title, string editorView)
        {
            Ensure.ParameterNotNullOrEmpty(title, "title");
            Ensure.ParameterNotNullOrEmpty(editorView, "editorView");

            Title = title;
            Description = string.Empty;
            EditorView = editorView;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayProviderEditorAttribute"/> class.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="editorView">
        /// The editor view.
        /// </param>
        public GatewayProviderEditorAttribute(string title, string description, string editorView)
        {
            Ensure.ParameterNotNullOrEmpty(title, "title");
            Ensure.ParameterNotNullOrEmpty(description, "description");
            Ensure.ParameterNotNullOrEmpty(editorView, "editorView");

            Title = title;
            Description = description;
            EditorView = editorView;
        }

        /// <summary>
        /// Gets the name of the gateway provider editor title  
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the description of the gateway provider editor 
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the relative path to the editor view html
        /// </summary>
        public string EditorView { get; private set; }
    }
}
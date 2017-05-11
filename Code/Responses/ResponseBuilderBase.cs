using System;

namespace XmlRpcClient.Responses
{
    /// <summary>
    /// Base for Response builder providing hacky way of getting right builder from collection.
    /// </summary>
    public abstract class ResponseBuilderBase
    {
        /// <summary>
        /// Gets type that this builder can build
        /// </summary>
        public Type ResponseType { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="supportedType">Type supported by builder</param>
        protected ResponseBuilderBase( Type supportedType )
        {
            ResponseType = supportedType;
        }
    }
}
using System;
using System.Linq;

namespace XmlRpcClient.Responses
{
    /// <summary>
    /// Contains collection of response builders for different types of Response.
    /// </summary>
    public class ResponseBuilderProvider
    {
        private readonly ResponseBuilderBase[] _responseBuilders;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="responseBuilders">Collection of ResponseBuilders</param>
        public ResponseBuilderProvider( params ResponseBuilderBase[] responseBuilders )
        {
            _responseBuilders = responseBuilders;
        }

        /// <summary>
        /// TODO: seems bit too hacky Barbara would be sad
        /// </summary>
        /// <typeparam name="T">Type of builder to acquire</typeparam>
        /// <returns></returns>
        public ResponseBuilder<T> GetBuilder<T>() where T : ResponseBase
        {
            var builder = (ResponseBuilder<T>) _responseBuilders.FirstOrDefault( x => x.ResponseType == typeof(T) );
            if ( builder == null )
            {
                throw new InvalidOperationException($"Builder for type: {typeof(T)} is not registered with this provider.");
            }
            return builder;
        }
    }
}

using System;
using System.IO;
using XmlRpcClient.Requests;
using XmlRpcClient.Responses;
using XmlRpcClient.Serialization;
using XmlRpcClient.Serialization.RawResponses;

namespace XmlRpcClient
{
    /// <summary>
    /// The simpliest way of RPC communication.
    /// </summary>
    public abstract class RpcClient : IRpcClient
    {
        private readonly IRequestSerializer _requestSerializer;
        private readonly IResponseDeserializer _responseDeserializer;
        private readonly ResponseBuilderProvider _builderProvider;
        
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="requestSerializer">Serializer for requests</param>
        /// <param name="responseDeserializer">Deserializer for responses</param>
        /// <param name="builderProvider">Builders to create concrete responses</param>
        protected RpcClient( IRequestSerializer requestSerializer,
                             IResponseDeserializer responseDeserializer,
                             ResponseBuilderProvider builderProvider )
        {
            if ( requestSerializer == null ) throw new ArgumentNullException( nameof( requestSerializer ) );
            if ( responseDeserializer == null ) throw new ArgumentNullException( nameof( responseDeserializer ) );
            if ( builderProvider == null ) throw new ArgumentNullException( nameof( builderProvider ) );

            _requestSerializer = requestSerializer;
            _responseDeserializer = responseDeserializer;
            _builderProvider = builderProvider;
        }

        /// <summary>
        /// Sends request to the server
        /// </summary>
        /// <typeparam name="T">Type of expected response</typeparam>
        /// <param name="requestMessage">The Request</param>
        /// <returns>Response from server built into concrete type</returns>
        /// <exception cref="ArgumentNullException">Request message is not provided.</exception>
        /// <exception cref="InvalidOperationException">Unable to build response (unregistered response builder).</exception>
        /// <exception cref="RpcException">Either request or response failed when accessing the web.</exception>
        public T SendRequest<T>( RpcRequestMessage requestMessage ) where T : ResponseBase
        {
            if ( requestMessage == null )
            {
                throw new ArgumentNullException( nameof( requestMessage ) );
            }

            var message = _requestSerializer.Serialize( requestMessage );
            var request = GetRequest();

            
            request.Send( message );
            var rawResponse = GetRawResponse( request );
            var concreteResponse = BuildConcreteResponse<T>( rawResponse );

            return concreteResponse;
        }

        /// <summary>
        /// Provides new instances of server-specific requests to used for sending messges.
        /// </summary>
        /// <returns>Request</returns>
        protected abstract RpcRequest GetRequest();

        private T BuildConcreteResponse<T>( RawResponse rawResponse ) where T : ResponseBase
        {
            var concreteBuilder = _builderProvider.GetBuilder<T>();
            
            return (T)concreteBuilder.GetResponse( rawResponse );
        }
        
        private RawResponse GetRawResponse( RpcRequest request )
        {
            
            using (var response = request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    return _responseDeserializer.Deserialize(responseStream);
                }
            }
        }
    }
}
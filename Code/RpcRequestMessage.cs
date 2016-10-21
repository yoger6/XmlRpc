using System.Collections.Generic;

namespace XmlRpcClient
{
    /// <summary>
    /// Message that can be serialized into RPC data format and sent to the server
    /// </summary>
    public class RpcRequestMessage
    {
        /// <summary>
        /// Name of method that request will call
        /// </summary>
        public string MethodName { get; }

        /// <summary>
        /// Request method parameters
        /// </summary>
        public List<RpcMessageParameter> Parameters { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="methodName">Name of the request method</param>
        public RpcRequestMessage( string methodName )
        {
            MethodName = methodName;
            Parameters = new List<RpcMessageParameter>();
        }

        /// <summary>
        /// Adds request parameter in the end of the list.
        /// </summary>
        /// <typeparam name="T">Type of parameter</typeparam>
        /// <param name="value">Value of parameter</param>
        public void AddParameter<T>( T value )
        {
            Parameters.Add( new RpcMessageParameter { ValueType = typeof( T ), Value = value } );
        }
    }
}
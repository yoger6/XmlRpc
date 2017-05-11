using XmlRpcClient.Requests;

namespace XmlRpcClient.Serialization
{
    /// <summary>
    /// Provides functionality to serialize RpcRequestMessages
    /// </summary>
    public interface IRequestSerializer
    {
        /// <summary>
        /// Serializes given request and returns it as a string.
        /// </summary>
        /// <param name="requestMessage">The request</param>
        /// <returns>Request serialized as string</returns>
        string Serialize( RpcRequestMessage requestMessage );
    }
}
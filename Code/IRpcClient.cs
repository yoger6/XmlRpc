using XmlRpcClient.Requests;
using XmlRpcClient.Responses;

namespace XmlRpcClient
{
    public interface IRpcClient
    {
        /// <summary>
        /// Sends request to the server
        /// </summary>
        /// <typeparam name="T">Type of expected response</typeparam>
        /// <param name="requestMessage">The Request</param>
        /// <returns>Response from server built into concrete type</returns>
        T SendRequest<T>( RpcRequestMessage requestMessage ) where T : ResponseBase;
    }
}
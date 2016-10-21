using System.IO;
using XmlRpcClient.Serialization.RawResponses;

namespace XmlRpcClient.Serialization
{
    /// <summary>
    /// Provides mechanism to deserialize response from RPC server
    /// </summary>
    public interface IResponseDeserializer
    {
        /// <summary>
        /// Deserializes given stream into RawResponse
        /// </summary>
        /// <param name="stream">Data to deserialize</param>
        /// <returns>RawResponse</returns>
        RawResponse Deserialize( Stream stream );
    }
}
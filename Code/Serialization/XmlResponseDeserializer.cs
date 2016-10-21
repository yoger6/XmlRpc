using System.IO;
using System.Xml.Serialization;
using XmlRpcClient.Serialization.RawResponses;

namespace XmlRpcClient.Serialization
{
    /// <summary>
    /// Deserializes given XML RPC response messages into objects.
    /// </summary>
    public class XmlResponseDeserializer : IResponseDeserializer
    {
        private readonly XmlSerializer _serializer = new XmlSerializer( typeof(RawResponse) );
        
        /// <summary>
        /// Deserializes given stream into RawResponse
        /// </summary>
        /// <param name="stream">Data to deserialize</param>
        /// <returns>RawResponse</returns>
        public RawResponse Deserialize( Stream stream )
        {
            return (RawResponse)_serializer.Deserialize( stream );
        }
    }
}

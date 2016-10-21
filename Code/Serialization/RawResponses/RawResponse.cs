using System.Xml.Serialization;

namespace XmlRpcClient.Serialization.RawResponses
{
    [XmlType( "methodResponse" )]
    public class RawResponse
    {
        [XmlElement("params")]
        public RawResponseParameters Parameters { get; set; }
    }
}
using System.Xml.Serialization;

namespace XmlRpcClient.Serialization.RawResponses
{
    [XmlType( "value" )]
    public class RawResponseParameterValue
    {
        [XmlElement("struct")]
        public RawResponseStruct RawResponseStruct { get; set; }
    }
}
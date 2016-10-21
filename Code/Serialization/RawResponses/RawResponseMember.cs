using System.Xml.Serialization;

namespace XmlRpcClient.Serialization.RawResponses
{
    [XmlType("member")]
    public class RawResponseMember
    {
        [XmlElement( "name" )]
        public string Name { get; set; }

        [XmlElement( "value" )]
        public RawResponseMemberValue Value { get; set; }
    }
}
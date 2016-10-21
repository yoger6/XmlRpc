using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlRpcClient.Serialization.RawResponses
{
    [XmlType("struct")]
    public class RawResponseStruct
    {
        [XmlElement("member")]
        public List<RawResponseMember> Members { get; set; }
    }
}
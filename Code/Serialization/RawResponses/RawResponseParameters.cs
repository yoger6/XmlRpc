using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlRpcClient.Serialization.RawResponses
{
    [XmlType( "params" )]
    public class RawResponseParameters
    {
        [XmlElement("param")]
        public List<RawResponseParameter> Parameters { get; set; }
    }
}
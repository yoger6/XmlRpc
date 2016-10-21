using System.Xml.Serialization;

namespace XmlRpcClient.Serialization.RawResponses
{
    [XmlType("param")]
    public class RawResponseParameter
    {
        [XmlElement("value")]
        public RawResponseParameterValue ParameterValue { get; set; }
    }
}
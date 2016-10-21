using System.Xml.Serialization;

namespace XmlRpcClient.Serialization.RawResponses
{
    [XmlType( IncludeInSchema = false )]
    public enum RpcValueTypes
    {
        @string,
        @int,
        i4,
        boolean,
        @datetime,
        @double,
        base64,
        array,
        @struct
    }
}
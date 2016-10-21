using System;
using System.Xml.Serialization;

namespace XmlRpcClient.Serialization.RawResponses
{
    public class RawResponseMemberValue
    {
        [XmlChoiceIdentifier( "RpcValueChoice" )] 
            [XmlElement( "i4", typeof( int ) ), 
             XmlElement( "int", typeof( int ) ), 
             XmlElement( "boolean", typeof( bool ) ), 
             XmlElement( "string", typeof( string ) ),
             XmlElement( "double", typeof( double ) ), 
             XmlElement( "datetime", typeof( DateTime ) ), 
             XmlElement( "base64", typeof( long ) ), 
             XmlElement( "array", typeof( object[] ) ), 
             XmlElement( "struct", typeof(RawResponseStruct) )]
        public object Value { get; set; }

        [XmlIgnore]
        public RpcValueTypes RpcValueChoice { get; set; }
    }
}
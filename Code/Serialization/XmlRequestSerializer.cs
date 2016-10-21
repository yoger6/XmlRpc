using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Common.Utilities;

namespace XmlRpcClient.Serialization
{
    /// <summary>
    /// Serializes Xml RPC messages.
    /// </summary>
    public class XmlRequestSerializer : IRequestSerializer
    {
        private const string Root = "methodCall";
        private const string MethodNameElementName = "methodName";
        private const string ParametersElementName = "params";
        private const string ParameterElementName = "param";

        /// <summary>
        /// Serializes given request and returns it as a string.
        /// </summary>
        /// <param name="requestMessage">The request</param>
        /// <returns>Request serialized as string</returns>
        public string Serialize( RpcRequestMessage requestMessage )
        {
            if ( requestMessage == null ) throw new ArgumentNullException( nameof( requestMessage ) );
            
            var element = new XElement( Root,
                new XElement( MethodNameElementName, GetMethodName( requestMessage ) ),
                new XElement( ParametersElementName, 
                    from parameter in requestMessage.Parameters 
                    select GetElementForParameter(parameter)
            ));

            return element.ToString( SaveOptions.DisableFormatting);
        }

        private XElement GetElementForParameter( RpcMessageParameter parameter )
        {
            var param = new XElement( ParameterElementName );

            param.Add( GetValueForParameter( parameter ) );

            return param;
        }

        private XElement GetValueForParameter( RpcMessageParameter parameter )
        {
            var valueRoot = new XElement( "value" );
            
            if (IsCollection( parameter.ValueType ))
            {
                var arrayRoot = new XElement( "array" );
                valueRoot.Add( arrayRoot );
                var dataRoot = new XElement( "data" );
                arrayRoot.Add( dataRoot );
                //TODO: maybe fix that already :P
                //But it works
                //TODO: doesn't matter
                foreach ( var value in parameter.Value as IEnumerable<string> )
                {
                    var rpc = new RpcMessageParameter( value );
                    dataRoot.Add( GetValueForParameter( rpc ) );
                }
            }
            else
            {
                var valueType = new XElement( GetValueTypeName( parameter.ValueType ) );
                valueRoot.Add( valueType );

                var actualValue = parameter.Value?.ToString() ?? string.Empty;
                valueType.Value = actualValue;
            }

            return valueRoot;
        }

        //TODO: for now works just with string collections
        private bool IsCollection( Type valueType )
        {
            return typeof(IEnumerable<string>).IsAssignableFrom( valueType );
        }

        private string GetValueTypeName( Type valueType )
        {
            var typeSwitch = new TypeSwitch<string>( () => "string" );

            typeSwitch.Set<int>( () => "int" );

            return typeSwitch.Execute( valueType );
        }

        private string GetMethodName( RpcRequestMessage message )
        {
            if(string.IsNullOrWhiteSpace(message.MethodName) ) throw new InvalidOperationException("Cannot send request without method name");
            return message.MethodName;
        }
    }
}
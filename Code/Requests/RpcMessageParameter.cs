using System;

namespace XmlRpcClient.Requests
{
    public class RpcMessageParameter
    {
        public string Name { get; set; }
        public Type ValueType { get; set; }
        public object Value { get; set; }

        public RpcMessageParameter()
        {
        }

        public RpcMessageParameter( object value )
        {
            Value = value;
            ValueType = value.GetType();
        }

        public RpcMessageParameter(string name, object value )
        {
            Name = name;
            Value = value;
            ValueType = value.GetType();
        }
    }
}
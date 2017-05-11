using System;

namespace XmlRpcClient
{
    public class RpcException : Exception
    {
        public RpcException()
        {
        }

        public RpcException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

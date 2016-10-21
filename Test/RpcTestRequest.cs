using XmlRpcClient;

namespace Test.XmlRpcClient
{
    internal class RpcTestRequest : RpcRequest
    {
        public RpcTestRequest( IHttpRequestFactory requestFactory) 
            : base( requestFactory, "google.com", "api", 81, "https" )
        {
        }
    }
}
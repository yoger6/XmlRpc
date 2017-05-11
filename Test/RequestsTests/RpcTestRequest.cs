using XmlRpcClient.Requests;

namespace Test.XmlRpcClient.RequestsTests
{
    internal class RpcTestRequest : RpcRequest
    {
        public RpcTestRequest( IHttpRequestFactory requestFactory) 
            : base( requestFactory, "google.com", "api", 81, "https" )
        {
        }
    }
}
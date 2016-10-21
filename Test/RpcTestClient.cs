using System;
using System.Net;
using Common.UnitTesting;
using Moq;
using XmlRpcClient;
using XmlRpcClient.Serialization;

namespace Test.XmlRpcClient
{
    internal class RpcTestClient : RpcClient
    {
        /// <summary>
        /// Request that is returned when GetRequest method is called
        /// </summary>
        public RpcTestRequest Request { get; }

        /// <summary>
        /// Mock of the factory providing InnerRequest
        /// </summary>
        public Mock<IHttpRequestFactory> FactoryMock { get; }

        /// <summary>
        /// Mock of request that is provided by the factory
        /// </summary>
        public Mock<HttpWebRequest> InnerRequestMock { get; }

        /// <summary>
        /// Mock of response that will be returned from request
        /// </summary>
        public Mock<HttpWebResponse> RequestResponseMock { get; }

        /// <summary>
        /// Number of times the method GetRequest was called
        /// </summary>
        public int GetRequestCount { get; private set; }

        public RpcTestClient( IRequestSerializer requestSerializer, 
            IResponseDeserializer responseDeserializer, 
            ResponseBuilderProvider builderProvider ) 
            : base( requestSerializer, responseDeserializer, builderProvider )
        {
            RequestResponseMock = new Mock<HttpWebResponse>();
            InnerRequestMock = new Mock<HttpWebRequest>();
            InnerRequestMock.Setup( x => x.GetRequestStream() ).Returns( new SpyStream() );
            InnerRequestMock.Setup( x => x.GetResponse() ).Returns( RequestResponseMock.Object );
            FactoryMock = new Mock<IHttpRequestFactory>();
            FactoryMock.Setup( x => x.GetRequest( It.IsAny<Uri>() ) ).Returns( InnerRequestMock.Object );
            Request = new RpcTestRequest(FactoryMock.Object);
        }

        protected override RpcRequest GetRequest()
        {
            GetRequestCount++;

            return Request;
        }
    }
}
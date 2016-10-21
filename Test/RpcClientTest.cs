using System;
using Common.UnitTesting;
using Moq;
using NUnit.Framework;
using XmlRpcClient;
using XmlRpcClient.Serialization;

namespace Test.XmlRpcClient
{
    [TestFixture]
    public class RpcClientTest
    {
        private RpcTestClient _client;
        private Mock<IRequestSerializer> _serializerMock;
        private Mock<IResponseDeserializer> _deserializerMock;
        private RpcRequestMessage _message;
        private BaseResponseTestBuilder _builder;
        private ResponseBuilderProvider _builderProvider;

        [SetUp]
        public void Setup()
        {
            _message = new RpcRequestMessage( "Any" );
            _serializerMock = new Mock<IRequestSerializer>();
            _deserializerMock = new Mock<IResponseDeserializer>();
            _deserializerMock.Setup( x => x.Deserialize( It.IsAny<SpyStream>() ) ).Returns( new TestRawResponse() );
            _builder = new BaseResponseTestBuilder();
            _builderProvider = new ResponseBuilderProvider( _builder );

            _client = new RpcTestClient(_serializerMock.Object, _deserializerMock.Object, _builderProvider );
        }

        [Test]
        public void Ctor_Throws_OnAnyParameterNull()
        {
            ConstructorAssert.ThrowsOnAnyNullArgument<RpcTestClient>(_serializerMock.Object, _deserializerMock.Object, _builderProvider);
        }

        [Test]
        public void SendRequest_ThrowsIfMessageIsNull()
        {
            TestDelegate nullMessage = () => _client.SendRequest<ResponseBase>( null );

            Assert.Throws<ArgumentNullException>( nullMessage );
        }

        [Test]
        public void SendRequest_GetsRequestThroughAbstractMethod()
        {
            SendSimpleRequest();

            Assert.AreEqual( 1, _client.GetRequestCount );
        }

        [Test]
        public void SendRequest_SerializesRequest()
        {
            SendSimpleRequest();

            _serializerMock.Verify( x => x.Serialize( _message ) );
        }

        [Test]
        public void SendRequest_SendsItViaRpcRequest()
        {
            SendSimpleRequest();

            _client.InnerRequestMock.Verify(x=>x.GetRequestStream());
        }

        [Test]
        public void SendRequest_ReadsResponse()
        {
            SendSimpleRequest();

            _client.InnerRequestMock.Verify( x => x.GetResponse() );
        }

        [Test]
        public void SendRequest_DeserializesResponse()
        {
            SendSimpleRequest();

            _deserializerMock.Verify(x=>x.Deserialize( It.IsAny<SpyStream>() ));
        }

        [Test]
        public void SendRequest_Throws_WhenNoSuitableBuilderIsFound()
        {
            TestDelegate requestTypeWithNoBuilderAvailable =
                () => _client.SendRequest<ResponseThatHasNoBuilder>( _message );

            Assert.Throws<InvalidOperationException>( requestTypeWithNoBuilderAvailable );
        }

        [Test]
        public void SendRequest_BuildsResponse()
        {
            SendSimpleRequest();

            Assert.True( _builder.BuildOccured );
        }

        [Test]
        public void SendRequest_ReturnsConcreteResponse()
        {
            var response = _client.SendRequest<ResponseBase>( _message );

            Assert.NotNull( response );
        }

        private void SendSimpleRequest()
        {
            _client.SendRequest<ResponseBase>( _message );
        }
    }
}

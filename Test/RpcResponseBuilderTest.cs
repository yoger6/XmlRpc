using System;
using NUnit.Framework;
using XmlRpcClient;

namespace Test.XmlRpcClient
{
    [TestFixture]
    public class RpcResponseBuilderTest
    {
        private TestRawResponse _rawResponse;
        private ResponseBuilder<ResponseBase> _builder;

        [SetUp]
        public void Setup()
        {
            _rawResponse = new TestRawResponse();
            _builder = new ResponseBuilder<ResponseBase>();
        }

        [Test]
        public void Ctor_DefinesTypeSupportedByBuilder()
        {
            Assert.AreSame( typeof(ResponseBase), _builder.ResponseType );
        }

        [Test]
        public void GetResponse_Throws_WhenResponseIsNull()
        {
            TestDelegate nullResponseGenerate = () => _builder.GetResponse( null );

            Assert.Throws<ArgumentNullException>( nullResponseGenerate );
        }
        
        [Test]
        public void GetResponse_ReturnsExpectedObject()
        {
            var actual = _builder.GetResponse(_rawResponse);

            Assert.AreEqual( TestRawResponse.ExpectedStatus, actual.RequestStatus );
            Assert.AreEqual( TestRawResponse.ExpectedTime, actual.RequestTime );
        }
    }
}
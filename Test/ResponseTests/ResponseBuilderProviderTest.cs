using System;
using NUnit.Framework;
using XmlRpcClient.Responses;

namespace Test.XmlRpcClient.ResponseTests
{
    [TestFixture]
    public class ResponseBuilderProviderTest
    {
        private ResponseBuilderProvider _provider;
        private BaseResponseTestBuilder _builder;

        [SetUp]
        public void Setup()
        {
            _builder = new BaseResponseTestBuilder();
            _provider = new ResponseBuilderProvider( _builder );
        }

        [Test]
        public void GetBuilder_Throws_WhenTryingToGetBuilderThatIsNotRegistered()
        {
            TestDelegate getNonExistingBuilder = () => _provider.GetBuilder<ResponseThatHasNoBuilder>();

            Assert.Throws<InvalidOperationException>( getNonExistingBuilder );
        }

        [Test]
        public void GetBuilder_ReturnsRegisteredBuilder_BySupportedResponseType()
        {
            var builder = _provider.GetBuilder<ResponseBase>();

            Assert.AreSame( _builder, builder );
        }
    }
}

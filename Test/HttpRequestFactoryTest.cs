using System;
using System.Net;
using NUnit.Framework;
using XmlRpcClient;

namespace Test.XmlRpcClient
{
    [TestFixture]
    public class HttpRequestFactoryTest
    {
        private HttpRequestFactory _factory = new HttpRequestFactory();
        private Uri _validUri = new Uri( "http://google.com" );

        [Test]
        public void GetRequest_ReturnsRequest_ThatIsNotNull()
        {
            var request = _factory.GetRequest( _validUri );

            Assert.NotNull( request );
        }

        [Test]
        public void GetRequest_ReturnsRequest_ForGivenUri()
        {
            var request = _factory.GetRequest( _validUri );

            Assert.AreSame( request.RequestUri, _validUri );
        }

        [Test]
        public void Ctor_SetsExpect100ContinueToFalse()
        {

            var actual = ServicePointManager.Expect100Continue;

            Assert.False( actual );
        }
    }
}

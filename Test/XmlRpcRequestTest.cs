using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using Common.UnitTesting;
using Moq;
using NUnit.Framework;
using XmlRpcClient;

namespace Test.XmlRpcClient
{
    [TestFixture]
    public class XmlRpcRequestTest
    {
        private const string Host = "api.unknown.com";
        private const string Path = "xml-rpc";
        private const string Scheme = "https";
        private const int Port = 443;

        private Mock<IHttpRequestFactory> _factory;
        private SpyStream _requestSpyStream;
        private Mock<HttpWebRequest> _requestMock;

        [SetUp]
        public void Setup()
        {
            _requestSpyStream = new SpyStream();
            var headers = new WebHeaderCollection();
            _requestMock = new Mock<HttpWebRequest>();
            _requestMock.Setup( x => x.Headers ).Returns( headers );
            _requestMock.Setup( x => x.GetRequestStream() ).Returns( _requestSpyStream );
            _factory = new Mock<IHttpRequestFactory>();
            _factory.Setup( x => x.GetRequest( GetValidHttpsUri() ) ).Returns( _requestMock.Object );
        }
        
        [Test]
        public void Ctor_SetsHttpsUri()
        {
            var request = GetRealRequest();

            Assert.AreSame( Uri.UriSchemeHttps, request.Uri.Scheme );
        }

        [Test]
        public void Ctor_CreatesValidUri()
        {
            var request = GetRealRequest();

            Assert.AreEqual( GetValidHttpsUri(), request.Uri );
        }

        [Test]
        public void Ctor_GetsWebRequestFromFactory()
        {
            var request = GetMockedRequest();

            _factory.Verify( x => x.GetRequest( GetValidHttpsUri() ) );
        }

        [Test]
        public void Ctor_CompressionDisabledByDefault()
        {
            var request = GetRealRequest();

            Assert.False( IsCompressionEnabled( request ) );
        }

        [Test]
        public void Ctor_EnableCompression_IncludesGzipCompressionHeaders()
        {
            var request = new RpcRequest( Host, Path, Port, Scheme, true );

            Assert.True( IsCompressionEnabled( request ) );
        }

        private bool IsCompressionEnabled(RpcRequest request)
        {
            return request.Headers[HttpRequestHeader.AcceptEncoding] == "gzip"
                && request.Headers[HttpRequestHeader.ContentEncoding] == "gzip";
        }

        [Test]
        public void Ctor_RequestMethod_IsPost()
        {
            var request = GetRealRequest();

            Assert.AreEqual( "POST", request.Method );
        }

        [Test]
        public void Send_WritesRequestToTheStream()
        {
            var expected = "Thats little bit weird";

            var request = GetMockedRequest();

            request.Send( expected );

            var actual = DecompressWrittenData( _requestSpyStream );

            Assert.AreEqual( expected, actual );
        }

        private string DecompressWrittenData( Stream stream )
        {
            using (var decompressionStream = new GZipStream( stream, CompressionMode.Decompress ))
            {
                using (var reader = new StreamReader( decompressionStream ))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        [Test]
        public void GetResponse_ReturnsResponseFromHttpRequest()
        {
            var request = GetMockedRequest();

            using (var response = request.GetResponse())
            {
                _requestMock.Verify( x => x.GetResponse() );
            }
        }
        
        private Uri GetValidHttpsUri()
        {
            var builder = new UriBuilder( Scheme, Host, Port, Path );

            return builder.Uri;
        }

        private RpcRequest GetMockedRequest()
        {
            return new RpcRequest( _factory.Object, Host, Path, Port, Scheme );
        }

        private RpcRequest GetRealRequest()
        {
            return new RpcRequest( Host, Path, Port, Scheme );
        }
    }
}

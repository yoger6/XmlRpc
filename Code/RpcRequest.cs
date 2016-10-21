using System;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace XmlRpcClient
{
    /// <summary>
    /// HttpWebRequest wrapper specificly designed for XML-RPC requests
    /// </summary>
    public class RpcRequest
    {
        private const string EncodingHeaderValue = "gzip";
        private const string DefaultMethod = "POST";

        protected HttpWebRequest Request;

        /// <summary>
        /// Requests Uri
        /// </summary>
        public Uri Uri => Request.RequestUri;

        /// <summary>
        /// Requests Headers
        /// </summary>
        public WebHeaderCollection Headers => Request.Headers;

        /// <summary>
        /// Request Method
        /// </summary>
        public string Method => Request.Method;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="host">Host address</param>
        /// <param name="path">Request path address</param>
        /// <param name="port">Service port</param>
        /// <param name="httpProtocol">Protocol used for connection</param>
        /// <param name="enableCompression">Enables gzip compression</param>
        public RpcRequest( string host, string path, int port, string httpProtocol, bool enableCompression = false )
            : this( new HttpRequestFactory(), host, path, port, httpProtocol, enableCompression )
        {
        }

        /// <summary>
        /// Constructor allowing custom request factory to be used.
        /// </summary>
        /// <param name="requestFactory">Factory generating requests</param>
        /// <param name="host">Host address</param>
        /// <param name="path">Request path address</param>
        /// <param name="port">Service port</param>
        /// <param name="httpProtocol">Protocol used for connection</param>
        /// <param name="enableCompression">Enables gzip compression</param>
        public RpcRequest( IHttpRequestFactory requestFactory, string host, string path, int port, string httpProtocol, bool enableCompression = false )
        {
            Request = requestFactory.GetRequest( GetValidHttpsUri( httpProtocol, host, path, port ) );
            SetupRequest();

            if ( enableCompression )
            {
                EnableCompression();
            }
        }

        /// <summary>
        /// Sends given text via request stream
        /// </summary>
        /// <param name="data">Text to send</param>
        public void Send( string data )
        {
            using (var compressedStream = new GZipStream( Request.GetRequestStream(), CompressionMode.Compress ))
            {
                using (var writer = new StreamWriter( compressedStream ))
                {
                    writer.Write( data );
                }
            }
        }

        /// <summary>
        /// Returns response from server
        /// </summary>
        /// <returns>Web Response</returns>
        public WebResponse GetResponse()
        {
            return Request.GetResponse();
        }

        private void SetupRequest()
        {
            Request.Method = DefaultMethod;
            Request.KeepAlive = false;
        }
        //TODO: extract CompressedRpcRequest?
        private void EnableCompression()
        {
            Request.Headers.Add( HttpRequestHeader.AcceptEncoding, EncodingHeaderValue );
            Request.Headers.Add( HttpRequestHeader.ContentEncoding, EncodingHeaderValue );
            Request.AutomaticDecompression = DecompressionMethods.GZip;
        }

        private Uri GetValidHttpsUri(string scheme, string host, string path, int port)
        {
            var builder = new UriBuilder( scheme, host, port, path );

            return builder.Uri;
        }
    }
}
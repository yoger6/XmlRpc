using System;
using System.Net;

namespace XmlRpcClient.Requests
{
    /// <summary>
    /// Default request factory, provides requests from static WebRequest factory method.
    /// </summary>
    public class HttpRequestFactory : IHttpRequestFactory
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public HttpRequestFactory()
        {
            ServicePointManager.Expect100Continue = false;
        }

        /// <summary>
        /// Gets request
        /// </summary>
        /// <param name="requestUri">Uri for request</param>
        /// <returns>HttpWebRequest</returns>
        public HttpWebRequest GetRequest( Uri requestUri )
        {
            return (HttpWebRequest)WebRequest.Create( requestUri );
        }
    }
}
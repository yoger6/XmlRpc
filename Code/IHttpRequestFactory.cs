using System;
using System.Net;

namespace XmlRpcClient
{
    /// <summary>
    /// Provides convinient way for getting HttpWebRequest
    /// </summary>
    public interface IHttpRequestFactory
    {
        /// <summary>
        /// Gets request
        /// </summary>
        /// <param name="requestUri">Uri for request</param>
        /// <returns>HttpWebRequest</returns>
        HttpWebRequest GetRequest( Uri requestUri );
    }
}
using System.Net;

namespace XmlRpcClient.Responses
{
    /// <summary>
    /// Represents basic data retrieved with request.
    /// </summary>
    public class ResponseBase
    {
        /// <summary>
        /// Status code provided by remote server
        /// </summary>
        public HttpStatusCode RequestStatus { get; set; }

        /// <summary>
        /// Request time completion on server side
        /// </summary>
        public double RequestTime { get; set; }
    }
}
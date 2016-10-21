using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using XmlRpcClient.Serialization.RawResponses;

namespace XmlRpcClient
{
    /// <summary>
    /// Builder class for converting RawResponse into structured one that derives from ResponseBase
    /// </summary>
    /// <typeparam name="T">Target response type</typeparam>
    public class ResponseBuilder<T> : ResponseBuilderBase where T : ResponseBase
    {
        private const string RequestTimeName = "seconds";
        private const string RequestStatusName = "status";
        private const string RegexFormulaNonNumeric = "[^0-9]";
        
        /// <summary>
        /// Raw response to get members from
        /// </summary>
        protected RawResponse RawResponse;

        /// <summary>
        /// Shortcut property to RawResponseMember collection within RawResponse
        /// </summary>
        protected List<RawResponseMember> Members;

        /// <summary>
        /// Response that is Build by this class
        /// </summary>
        protected T Response;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ResponseBuilder()
            : base(typeof(T))
        {
        }

        /// <summary>
        /// Returns response that was built by this object.
        /// Make sure to call Build() before trying to get it.
        /// </summary>
        /// <returns>Concrete response</returns>
        public T GetResponse(RawResponse rawResponse)
        {
            if ( rawResponse == null )
            {
                throw new ArgumentNullException( nameof( rawResponse ) );
            }

            RawResponse = rawResponse;
            GenerateShortcutForMembers( rawResponse );

            Build();

            return Response;
        }

        /// <summary>
        /// Builds concrete Response based on RawResponse.
        /// Make sure to call base first as it creates the response and assings status code/request time to it.
        /// </summary>
        protected virtual void Build()
        {
            Response = Activator.CreateInstance<T>();

            SetStatusAndTime( Response );
        }

        /// <summary>
        /// Looks for a member with given name in top hierarchy member path and returns its value.
        /// </summary>
        /// <typeparam name="V">Type of desired value</typeparam>
        /// <param name="name">Name of member</param>
        /// <returns>The value</returns>
        protected V GetValue<V>( string name )
        {
            return GetValue<V>( name, Members );
        }

        /// <summary>
        /// Looks for a member with given name within collection of members and returns its value.
        /// </summary>
        /// <typeparam name="V">Type of desired value</typeparam>
        /// <param name="name">Name of member</param>
        /// <param name="members">Members to look among</param>
        /// <returns>The value</returns>
        protected V GetValue<V>( string name, IEnumerable<RawResponseMember> members )
        {
            var member = members.FirstOrDefault( x => x.Name == name );
            
            return (V)member?.Value.Value;
        }

        private void GenerateShortcutForMembers( RawResponse rawResponse )
        {
            //Members = rawResponse.Parameters.Parameters.First().RawResponseStruct.Members;
            Members = rawResponse.Parameters.Parameters.First().ParameterValue.RawResponseStruct.Members;
        }

        private void SetStatusAndTime( ResponseBase response )
        {
            response.RequestStatus = GetStatus();
            response.RequestTime = GetValue<double>( RequestTimeName );
        }

        private HttpStatusCode GetStatus()
        {
            var statusValue = GetValue<string>( RequestStatusName );
            statusValue = Regex.Replace( statusValue, RegexFormulaNonNumeric, "" );

            return (HttpStatusCode)Enum.Parse( typeof(HttpStatusCode), statusValue );
        }

    }
}
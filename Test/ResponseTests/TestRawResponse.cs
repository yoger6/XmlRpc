using System.Collections.Generic;
using System.Net;
using XmlRpcClient.Serialization.RawResponses;

namespace Test.XmlRpcClient.ResponseTests
{
    public class TestRawResponse : RawResponse
    {
        public const HttpStatusCode ExpectedStatus = HttpStatusCode.OK;
        public const double ExpectedTime = 0.008;

        public TestRawResponse()
        {
            Parameters = new RawResponseParameters {Parameters = new List<RawResponseParameter>()};

            var responseParameter = new RawResponseParameter();
            var responseStruct = new RawResponseStruct
            {
                Members = new List<RawResponseMember>
                {
                    new RawResponseMember
                    {
                        Name = "status",
                        Value = new RawResponseMemberValue {RpcValueChoice = RpcValueTypes.@string, Value = "200 OK"}
                    },
                    new RawResponseMember
                    {
                        Name = "seconds",
                        Value =
                            new RawResponseMemberValue {RpcValueChoice = RpcValueTypes.@double, Value = ExpectedTime}
                    }
                }
            };
            responseParameter.ParameterValue = new RawResponseParameterValue();
            responseParameter.ParameterValue.RawResponseStruct = responseStruct;
            Parameters.Parameters.Add( responseParameter );
        }
    }
}
using XmlRpcClient.Responses;

namespace Test.XmlRpcClient.ResponseTests
{
    internal class BaseResponseTestBuilder : ResponseBuilder<ResponseBase>
    {
        public bool BuildOccured { get; private set; }

        protected override void Build()
        {
            base.Build();

            BuildOccured = true;
        }
    }
}
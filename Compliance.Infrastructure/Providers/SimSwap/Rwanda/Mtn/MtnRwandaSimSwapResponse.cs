using Compliance.Domain.Response;

namespace Compliance.Infrastructure.Providers.Rwanda.Mtn
{
    public class MtnRwandaSimSwapResponse : SimSwapResponse
    {
        public string ResultCode { get; set; }
        public string ResultDescription { get; set; }
        public List<SimSwapResponseObject> Data { get; set; }

        public override bool IsSuccessful
        {
            get
            {
                return ResultCode == "0000";
            }
        }
    }

    public class SimSwapResponseObject
    {
        public DateTime CompletionDate { get; set; }
    }
}
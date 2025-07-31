using Compliance.Domain.Response;

namespace Compliance.Infrastructure.Providers.Rwanda.Mtn
{
    public class AirtelRwandaSimSwapResponse : SimSwapResponse
    {
        public Response Response { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class Response
    {
        public string DateTime { get; set; }
        public bool IsSimSwapped { get; set; }
        public string Msisdn { get; set; }
        public string TransactionId { get; set; }
    }
}
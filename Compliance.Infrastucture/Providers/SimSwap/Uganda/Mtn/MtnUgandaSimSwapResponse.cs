using Compliance.Domain.Response;
using System.Text.Json.Serialization;


namespace Compliance.Infrastructure.Providers.Uganda.Mtn
{
    public class MtnUgandaSimSwapResponse : SimSwapResponse
    {

        [JsonPropertyName("data")]
        public Data Data { get; set; }

        [JsonPropertyName("customerId")]
        public string CustomerId { get; set; }

        [JsonPropertyName("transactionId")]
        public string TransactionId { get; set; }

        [JsonPropertyName("statusMessage")]
        public string StatusMessage { get; set; }

        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; }

        public override bool IsSuccessful => StatusCode == "0000" || StatusCode == "1022";
        public override DateTime? LastSwap => Data.SimSwapDate;
    }

    public class Data
    {
        [JsonPropertyName("lastSimSwapDate")]
        public string LastSimSwapDate { get; set; }

        public DateTime? SimSwapDate
        {
            get
            {
                if (string.IsNullOrEmpty(LastSimSwapDate)) return null;
                DateTime.TryParse(LastSimSwapDate, out DateTime lastSimSwapDate);
                return lastSimSwapDate;
            }
        }
    }

}
using System.Text.Json.Serialization;

namespace Compliance.Domain.Response;

public class SimSwapResponse
{
           
    public virtual bool IsSuccessful { get; set; }
    public bool IsSwaped { get; set; }
    public virtual DateTime? LastSwap { get; set; }
    public string PhoneNumber { get; set; }
    [JsonIgnore]
    public bool ApiReached { get; set; } = false;
}
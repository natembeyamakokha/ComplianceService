namespace Compliance.Domain.Response;

public class BulkSimSwapResponse
{
    public bool Successful { get; set; } = false;
    public bool SimSwapChecksPassed { get; set; } = false;
    public SimSwapResponse[] SimSwapResponse {get; set; }
}
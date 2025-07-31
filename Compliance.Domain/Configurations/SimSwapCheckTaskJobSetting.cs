namespace Compliance.Domain.Configurations;

public class SimSwapCheckTaskJobSetting
{
    public required string JobKey { get; set; }
    public int IntervalMilliSeconds { get; set; } = 1000; // 1 sec
    public int MaxIntervalMilliSeconds { get; set; } = 30 * 1000; // 30 sec
    public bool IsActive { get; set; }
    public int NextRunHour { get; set; }
    public int MaximumAttemptCount { get; set; }
    public int MaxTaskCount { get; set; } = 100;
    public int TaskQueueCount { get; set; } = 5;
}
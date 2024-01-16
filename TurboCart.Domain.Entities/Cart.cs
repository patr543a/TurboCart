namespace TurboCart.Domain.Entities;

public class Cart
{
	public int CartNumber { get; set; }
	public string DriverName { get; set; } = string.Empty;
	public int LapNumber { get; set; }
	public TimeSpan LapTime { get; set; }
	public TimeSpan TotalTime { get; set; }
	public bool IsFinished { get; set; }
}

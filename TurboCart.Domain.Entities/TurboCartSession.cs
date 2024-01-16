namespace TurboCart.Domain.Entities;

public class TurboCartSession
{
	public bool IsActive { get; set; }
	public bool IsFinished { get; set; }
	public TimeSpan TotalTime { get; set; }
	public IEnumerable<Cart> Carts { get; set; } = [];
}

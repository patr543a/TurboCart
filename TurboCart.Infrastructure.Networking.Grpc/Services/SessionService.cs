using Grpc.Core;
using TurboCart.Domain.Entities;
using TurboCart.Infrastructure.Networking.Services;

namespace TurboCart.Infrastructure.Networking.Grpc.Services;

public class SessionService(
	ILogger<SessionService> _logger,
	ITurboCartSessionService _signalRService)
	: Session.SessionBase
{
    public override async Task<SessionReply> SessionChanged(SessionRequest request, ServerCallContext context)
	{
		var finished = request.Carts.All(c => c.IsFinished);

		await _signalRService.InvokeSessionChanged(new()
		{
			IsActive = !finished,
			IsFinished = finished,
			TotalTime = request.TotalTime.ToTimeSpan(),
			Carts = request.Carts.Select(c => new Cart()
			{
				CartNumber = c.CartNumber,
				DriverName = c.DriverName,
				LapNumber = c.LapNumber,
				LapTime = c.LapTime.ToTimeSpan(),
				TotalTime = c.TotalTime.ToTimeSpan(),
				IsFinished = c.IsFinished,
			}),
		});

		return new SessionReply();
	}
}

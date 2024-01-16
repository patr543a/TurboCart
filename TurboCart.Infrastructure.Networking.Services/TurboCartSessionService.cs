using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using TurboCart.Domain.Entities;

namespace TurboCart.Infrastructure.Networking.Services;

public class TurboCartSessionService(ILogger<TurboCartSessionService> _logger)
	: SignalRServiceBase(@"https://localhost:7014/Session", _logger), 
	ITurboCartSessionService
{
	public async Task InvokeSessionChanged(TurboCartSession arg1)
		=> await Invoke("SessionChanged", arg1);

	public void OnSessionChanged(Action<TurboCartSession> action)
		=> Listen("ReceiveSession", action);
}

public interface ITurboCartSessionService
{
	Task InvokeSessionChanged(TurboCartSession arg1);
	void OnSessionChanged(Action<TurboCartSession> action);
	Task Connect();
	HubConnection Connection { get; }
}
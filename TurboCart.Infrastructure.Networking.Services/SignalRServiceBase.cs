using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace TurboCart.Infrastructure.Networking.Services;

public abstract class SignalRServiceBase(string url, ILogger logger)
{
    private readonly HubConnection _connection = new HubConnectionBuilder()
        .WithAutomaticReconnect()
        .WithUrl(url)
        .Build();

    public HubConnection Connection => _connection;

    protected void Listen<T1>(string methodName, Action<T1> func)
        => _connection.On(methodName, func);

    protected async Task Invoke(string methodName, object arg1, Action<Exception>? onError = null)
    {
        await Connect();

		try
		{
			await _connection.InvokeAsync(methodName, arg1);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to invoke");

            onError?.Invoke(ex);
		}
	}

    public async Task Connect()
    {
        if (_connection.State == HubConnectionState.Connected)
            return;

        try
        {
            await _connection.StartAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to connect");
        }
    }
}

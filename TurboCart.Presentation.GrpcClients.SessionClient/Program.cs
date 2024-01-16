using Grpc.Net.Client;
using TurboCart.Presentation.GrpcClient.SessionClient;
using TurboCart.Presentation.GrpcClients.SessionClient.Simulator;


while (true)
{
	var simulator = new LapTimeSimulator();

	var karts = new List<Kart>()
	{
		new("Test 1", 1),
		new("Test 2", 2),
		new("Test 3", 3),
		new("Test 4", 4),
		new("Test 5", 5),
	};

	_ = simulator.SimulateRealTime(karts, 5, 4);

	SessionRequest? request = null;

    while (!request?.Carts.All(c => c.IsFinished) ?? true)
		await SendRequest(request = simulator.CreateSessionRequest(karts), 1000);

	for (var i = 0; i < 5; i++)
		await SendRequest(request = simulator.CreateSessionRequest(karts), 5000);
}

static async Task SendRequest(SessionRequest request, int duration)
{
	try
	{
		var channel = GrpcChannel.ForAddress("https://localhost:7014");

		var client = new Session.SessionClient(channel);

		await client.SessionChangedAsync(request);
	}
	catch { }

	Thread.Sleep(duration);
}
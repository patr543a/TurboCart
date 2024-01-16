using Google.Protobuf.WellKnownTypes;
using TurboCart.Presentation.GrpcClient.SessionClient;

namespace TurboCart.Presentation.GrpcClients.SessionClient.Simulator;

internal class LapTimeSimulator
{
    internal static readonly string LapTimeFormatSpecifier = @"mm\:ss\.ffff";
    private static DateTime startTime;
    private static int currentLap = 1;
    private static int counter = 1;
    private readonly TimeSpan targetLapTime = new(0, 0, 1, 40); // A magic number.
    public static event EventHandler CheckeredFlag;

    public async Task SimulateRealTime(List<Kart> karts, int durationInMinutes, double simSpeed, int sessionID = 1)
    {
        // Convert to TimeSpan so we can multiply with the simSpeed, and still use the TimeSpan:
        TimeSpan duration = TimeSpan.FromMinutes(durationInMinutes);
        duration /= simSpeed;

        // A delta is the time around a specific target time. So this is plus or minus 10% the target time. 10% because to get a spread of lap times that are "just right".
        double deltaPercent = 0.10;
        TimeSpan delta = targetLapTime * deltaPercent / simSpeed;

        int noOfCarts = karts.Count;

        // Make karts:
        for (int i = 0; i < noOfCarts; i++)
        {
            karts[i].Set(targetLapTime, delta, simSpeed);

            // Connect the crossed line event of each kart to the event handler:
            karts[i].CrossedTheLine += OnKartCrossedTheLineAsync;
        }

        // Set up parallel execution of each kart's  race:
        ParallelOptions parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = noOfCarts };
        Parallel.Invoke(new ParallelOptions() { MaxDegreeOfParallelism = 1 }, async () => await StartRaceClock(duration));

        // Execute the race by calling the Race method on each kart, in parallel:
        await Parallel.ForEachAsync(
            karts,
            parallelOptions,
            async (driver, ct) => { await driver.Race(); });
    }

    // Race clock tha "counts down" until the checkered flag is flown:
    private async Task StartRaceClock(TimeSpan duration)
    {
        var now = DateTime.Now;
        startTime = now;
        await Console.Out.WriteLineAsync($"{duration.ToString(LapTimeFormatSpecifier)} real time race started at [{now.ToString("HH:mm:ss")}]");
        await Task.Delay(duration);
        CheckeredFlag?.Invoke(this, new());
    }

    // Handle what happens when a kart crosses the line:
    private async void OnKartCrossedTheLineAsync(object sender, CrossedLineEventArgs e)
    {
        if (currentLap != e.Lap)
        {
            currentLap = e.Lap;
            Console.WriteLine();
        }
        await Console.Out.WriteLineAsync($"[{counter++}: {DateTime.Now.ToString("HH:mm:ss")}]\t" + e.ToString());
    }

    public SessionRequest CreateSessionRequest(List<Kart> karts)
    {
        var time = DateTime.Now;
        var timeSinceStart = time - startTime;

		var request = new SessionRequest
        {
            TotalTime = Duration.FromTimeSpan(
                karts.Select(c => c.IsFinished ? 
                    c.EndTime - startTime : 
                    timeSinceStart)
                .Max()),
        };

        request.Carts.AddRange(karts.Select(c => new SessionCart()
        {
            IsFinished = c.IsFinished,
            CartNumber = c.KartNo,
            DriverName = c.Name,
            LapNumber = c.CurrentLap,
            LapTime = Duration.FromTimeSpan(c.IsFinished ? c.EndTime - c.CurrentLapTime : time - c.CurrentLapTime),
            TotalTime = Duration.FromTimeSpan(c.IsFinished ? c.EndTime - startTime : time - startTime),
        }));

        return request;
    }
}

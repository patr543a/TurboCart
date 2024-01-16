namespace TurboCart.Presentation.GrpcClients.SessionClient.Simulator;

internal class Kart
{
    public int KartNo { get; set; }
    public string Name { get; set; }
    public DateTime CurrentLapTime { get; set; }
    public DateTime EndTime { get; set; }
	public TimeSpan TotalTime => totalTime;
    private Random generator = new();
    private TimeSpan delta;
    private TimeSpan targetLapTime;
    private TimeSpan totalTime;
    public int CurrentLap { get; set; }
    public event EventHandler<CrossedLineEventArgs> CrossedTheLine;
    private bool isLastLap;
    public bool IsFinished { get; set; }


	public Kart(string name, int no)
    {
        Name = name;
        KartNo = no;
    }

    public void Set(TimeSpan targetLapTime, TimeSpan delta, double fastness)
    {
        this.targetLapTime = TimeSpan.FromTicks((long)(targetLapTime.Ticks / fastness));
        this.delta = TimeSpan.FromTicks((long)(delta.Ticks / fastness));
        LapTimeSimulator.CheckeredFlag += OnCheckeredFlag;
    }

    private async void OnCheckeredFlag(object? sender, EventArgs e)
    {
        await Console.Out.WriteLineAsync($"Checkered flag for {KartNo}.");
        isLastLap = true;
    }

    public async Task Race()
    {
        CurrentLapTime = DateTime.Now;

        while (true)
        {
            // Calculate lap:
            long minDeltaTicks = targetLapTime.Ticks - delta.Ticks;
            long maxDeltaTicks = targetLapTime.Ticks + delta.Ticks;
            double p = generator.NextDouble();  // p is a probability between 0 and 1 (0% - 100%).
            if (p > 0.8) // 20% probability of a faster lap than the plus minus 10%
            {
                minDeltaTicks -= delta.Ticks;
            }
            else if (p < 0.35)   // 35% propability of a slower lap than the plus minus 10%
            {
                maxDeltaTicks += delta.Ticks;
            }

            // Get laptime as ticks between the new lower and upper bound:
            long diff = generator.NextInt64(minDeltaTicks, maxDeltaTicks);
            TimeSpan newLapTime = new TimeSpan(diff);

            // Add this new laptime the the total race time for this kart:
            totalTime += newLapTime;

            // one more lap
            ++CurrentLap;
            CurrentLapTime = DateTime.Now;

            // This effectively "does" the lap, by waiting to publish the new laptime until the time has passed:
            await Task.Delay(newLapTime);

            // Notify listeners (the simulator)
            CrossedLineEventArgs e = new() { KartNo = KartNo, Lap = CurrentLap, LapTime = newLapTime, TotalTime = totalTime };
            CrossedTheLine?.Invoke(this, e);

            // Terminating statement:
            if (isLastLap)
            {
				IsFinished = true;
                EndTime = DateTime.Now;

                return;
            }
        }
    }
}

internal class CrossedLineEventArgs : EventArgs
{
    public int KartNo { get; set; }
    public int Lap { get; set; }
    public TimeSpan LapTime { get; set; }
    public TimeSpan TotalTime { get; set; }

    public override string ToString()
    {
        return $"Kart No. {KartNo}\tLap {Lap}\tLap Time: {LapTime.ToString(LapTimeSimulator.LapTimeFormatSpecifier)}\tTotal Time: {TotalTime.ToString(LapTimeSimulator.LapTimeFormatSpecifier)}";
    }
}
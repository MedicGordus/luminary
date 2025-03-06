using System.Diagnostics;

namespace luminary.data.flux;

public class SubCentisecondTimer : FluxTimer
{
    /// <summary>
    /// The initial wait called to setup the timer for tuning.a
    /// </summary>
    public const int PREPARE_DEFAULT_MILLISECONDS = 500;

    /// <summary>
    /// The total amount of parallel threads to run.
    /// 
    /// Make sure this is high enough to pressure test, but not too high that the actual timer will never use that many.
    /// </summary>
    public const int TUNING_THREAD_COUNT = 10;

    /// <summary>
    /// The total duration of tuning, for each thread.
    /// </summary>
    public const int TUNING_DURATION_MILLISECONDS = 2000;

    /// <summary>
    /// How long the tuning delays run for.
    /// </summary>
    public const int TUNING_DELAY_MILLISECONDS = 100;

    /// <summary>
    /// The highest buffer amount to test. Windows uses 15-16ms, so a bit higher than this is the target.
    /// </summary>
    public const int MAX_MILLSECOND_BUFFER = 20;

    /// <summary>
    /// The maximum amount of drift allowed during tuning.
    //  - higher = less cpu use, less precise
    //  - lower = more intense cpu use, more precise
    /// </summary>
    public const int TUNE_MAX_DRIFT_MILLISECONDS = 5;

    /// <summary>
    /// The default buffer to spinwait (outside of async await)
    //  - higher = more intense cpu use, more precise
    //  - lower = less cpu use, less precise
    //
    // Running TuneBufferAsync() overwrites this based on benchmarks and TUNE_MAX_DRIFT_MILLISECONDS.
    /// </summary>
    public static int BufferMilliseconds = 7;

    /// <summary>
    /// This is process intensive and should only be run during startup.
    /// </summary>
    /// <returns></returns>
    public static async Task TuneBufferAsync()
    {
        Dictionary<int, long> tickCountPerTune = [];

        for(int deltaTune = MAX_MILLSECOND_BUFFER; deltaTune > 0; deltaTune--)
        {
            // make sure the class is setup
            await WaitAsync(PREPARE_DEFAULT_MILLISECONDS, BufferMilliseconds).ConfigureAwait(false);

            List<Task> tuningTasks = [];

            List<long> taskTicks = [.. new long[TUNING_THREAD_COUNT]];

            for(int deltaThread = 0; deltaThread < TUNING_THREAD_COUNT; deltaThread++)
            {
                int snapshotTuneNumber = deltaTune;
                int snapshotThreadNumber = deltaThread;
                tuningTasks.Add(
                    Task.Run(
                        async () => {
                            long totalDurationTicks = 0L;
                            int totalIterations = TUNING_DURATION_MILLISECONDS / TUNING_DELAY_MILLISECONDS;
                            for(int deltaIteration = 0; deltaIteration < totalIterations; deltaIteration++)
                            {
                                totalDurationTicks += await WaitAsync(TUNING_DELAY_MILLISECONDS, snapshotTuneNumber).ConfigureAwait(false);
                            }
                            //Console.WriteLine($"thread averaged {(double)totalDuration / (double)(totalIterations * TimeSpan.TicksPerMillisecond)} milliseconds, should be {TUNING_DELAY_MILLISECONDS}");
                            taskTicks[snapshotThreadNumber] = totalDurationTicks;
                        }
                    )
                );
            }

            await Task.WhenAll(tuningTasks).ConfigureAwait(false);


            long totalTicksForTune = 0L;
            foreach(long deltaTickCount in taskTicks)
            {
                totalTicksForTune += deltaTickCount;
            }

            tickCountPerTune.Add(deltaTune, totalTicksForTune);
        }

        // filter results that are sub centisecond
        Dictionary<int, long> subCentisecondResults = [];
        foreach(KeyValuePair<int, long> deltaEntry in tickCountPerTune)
        {
            long averaegeDurationMmilliseconds = deltaEntry.Value / (TUNING_THREAD_COUNT * (TUNING_DURATION_MILLISECONDS / TUNING_DELAY_MILLISECONDS) * TimeSpan.TicksPerMillisecond);
            Console.WriteLine($"Tune {deltaEntry.Key} resulted in average of {averaegeDurationMmilliseconds}ms");
            if(averaegeDurationMmilliseconds < (TUNING_DELAY_MILLISECONDS + TUNE_MAX_DRIFT_MILLISECONDS))
            {
                subCentisecondResults.Add(deltaEntry.Key, deltaEntry.Value);
            }
        }

        // use the lowest number so we spin as low as possible
        KeyValuePair<int, long> maxEfficiency = new KeyValuePair<int, long>(MAX_MILLSECOND_BUFFER, -1);
        foreach(KeyValuePair<int, long> deltaEntry in subCentisecondResults)
        {
            if(deltaEntry.Key < maxEfficiency.Key)
            {
                maxEfficiency = deltaEntry;
            }
        }

        BufferMilliseconds = maxEfficiency.Key;
    }

    public static async Task<long> WaitAsync(int _milliseconds, int _buffer)
    {
        if (_milliseconds <= 0) return 0;

        // Use Stopwatch for high-precision timing
        Stopwatch stopwatch = Stopwatch.StartNew();

        if(_buffer != 0)
        {
            // Wait using Task.Delay for the bulk of the time, leaving a small buffer
            if (_milliseconds > _buffer)
            {
                await Task.Delay(_milliseconds - _buffer).ConfigureAwait(false);
            }

            // Spin-wait for the remaining time to ensure precision
            while (stopwatch.ElapsedMilliseconds < _milliseconds)
            {
                // Tight loop for final precision
                Thread.SpinWait(1); // Very short spin to avoid excessive CPU usage
            }
        }
        else
        {
            await Task.Delay(_milliseconds).ConfigureAwait(false);
        }

        stopwatch.Stop();

        return stopwatch.ElapsedTicks;
    }
    
    public override Task<long> WaitAsync(int _milliseconds)
    {
        return WaitAsync(_milliseconds, BufferMilliseconds);
    }
}
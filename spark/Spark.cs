using luminary.mapping;
using luminary.util;

namespace luminary.spark;

public enum SparkState : int
{
    z_error = 0,

    NotStarted = 1,

    Running = 2,

    Completed = 3,

    Paniced = 4
}

public enum SparkType : int
{
    z_error = 0,

    Beacon = 1,

    Fiber = 2,

    Laser = 3,

    Mushroom = 4
}

/// <summary>
/// One spark is created for each event trigger (timer or http listener).
/// </summary>
public abstract class Spark
{
    /// <summary>
    /// Holds the current state of this Spark.
    /// </summary>
    public SparkState State;

    /// <summary>
    /// Holds the panic exception if the spark panics.
    /// </summary>
    public Exception? PanicException;

    /// <summary>
    /// Holds the prism that will be null until the trigger completes successfully.
    /// </summary>
    public Prism? Result;

    /// <summary>
    /// Use this whenever changing the state to avoid race conditions
    /// </summary>
    protected AsyncLock StateLock;

    /// <summary>
    /// Holds reference to the triggered task (once triggered, or null if not triggered).
    /// </summary>
    public Task? TriggeredTask;

    /// <summary>
    /// Holds reference to the mapping flow related to this event trigger.
    /// </summary>
    protected readonly MappingFlow Flow;

    /// <summary>
    /// Holds the input prism used for the mapping flow.
    /// </summary>
    protected readonly Prism FlowInput;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="_flow">Holds reference to the mapping flow related to this event trigger.</param>
    /// <param name="_input">Holds the input prism used for the mapping flow.</param>
    public Spark(MappingFlow _flow, Prism _input)
    {
        State = SparkState.NotStarted;
        PanicException = null;
        StateLock = AsyncLock.Create();
        TriggeredTask = null;
        Flow = _flow;
        FlowInput = _input;
    }

    /// <summary>
    /// Triggers the spark as expected.
    /// </summary>
    /// <returns>Panicable (if it failed or not).</returns>
    public async Task<Panicable> TriggerAsync()
    {
        Panicable output = new();

        using (await StateLock.LockAsync().ConfigureAwait(false))
        {
            if (State != SparkState.NotStarted)
            {
                output.ActivatePanic(
                    new Exception($"State is '{State}' when expected state was NotStarted - unable to trigger the Spark.")
                );

                return output;
            }

            State = SparkState.Running;

            // schedules a task on the thread pool
            //
            //  The task awaits the lock we are inside,
            //      but it shouldn't impact this since
            //      it should continue without delay.
            //
            TriggeredTask = Task.Run(TriggerWrapperAsync);
        }

        return output;
    }

    /// <summary>
    /// Wrapper for the trigger, handles updating the state, panic exception (if a panic occurs) and the result (if the spark completes successfully).
    /// </summary>
    /// <returns>n/a</returns>
    protected async Task TriggerWrapperAsync()
    {
        Panicable<Prism?> result = await TriggerDefinitionAsync().ConfigureAwait(false);

        using (await StateLock.LockAsync().ConfigureAwait(false))
        {
            if (result.Paniced)
            {
                PanicException = result.GetException();
                State = SparkState.Paniced;
            }
            else
            {
                Result = result.ReturnValue;
                State = SparkState.Completed;
            }
        }
    }

    /// <summary>
    /// Spark types handle the trigger process as per their spark type.
    /// </summary>
    /// <returns>N/A</returns>
    protected abstract Task<Panicable<Prism?>> TriggerDefinitionAsync();

    /// <summary>
    /// Helper call, creates and triggers the Spark as configured.
    /// </summary>
    /// <param name="_type">Type of spark.</param>
    /// <param name="_flow">Holds reference to the mapping flow related to this event trigger.</param>
    /// <param name="_input">Holds the input prism used for the mapping flow.</param>
    /// <returns></returns>
    public static async Task<Panicable<Spark>> CreateAndTriggerAsync(SparkType _type, MappingFlow _flow, Prism _input)
    {
        // create the non abstract spark
        Spark? spark = _type switch
        {
            SparkType.Beacon => new BeaconSpark(_flow, _input),
            SparkType.Fiber => new FiberSpark(_flow, _input),
            SparkType.Laser => new LaserSpark(_flow, _input),
            SparkType.Mushroom => new MushroomSpark(_flow, _input),
            _ => null
        };

        Panicable<Spark>? output;

        // ensure the spark type was valid
        if (spark == null)
        {
            output = new();
            output.ActivatePanic(
                new Exception($"Cannot create and trigger spark as type '{_type}' is unknown, expected 1, 2, 3, 4 (Beacon, Fiber, Laser, Mushroom).")
            );
        }
        else
        {
            // set the output
            output = new(spark);

            // get the result of the trigger
            Panicable result = await spark.TriggerAsync().ConfigureAwait(false);

            // forward the panic exception if it paniced
            if (result.Paniced)
            {
                output.ActivatePanic(result.GetException());
            }
        }

        // return the spark in the panicable
        return output;
    }
}
namespace luminary.util;


/// <summary>
/// Helper class for async returns to let their caller know to cancel whatever they were doing because of a failure.
/// </summary>
/// <remarks>
/// Usage:
///     public Task<Scrubbable<bool>> TestAsync() {}
///     
///     Scrubbable<bool> _test = await Test();
///     if(_test.Scrub)
///     {
///         //cancel
///     }
///     
///     // continue
/// </remarks>
public class Scrubbable<T> : Scrubbable
{
    /// <summary>
    /// The return value of the unscrubbed result.
    /// </summary>
    public T? ReturnValue;

    public Scrubbable(T? t = default) : base()
    {
        ReturnValue = t;
    }
}

/// <summary>
/// Helper class for async returns to let their caller know to cancel whatever they were doing because of a failure.
/// </summary>
/// <remarks>
/// Usage:
///     public Task<Scrubbable> TestAsync() {}
///     
///     Scrubbable _test = await Test();
///     if(_test.Scrub)
///     {
///         //cancel
///     }
///     
///     // continue
/// </remarks>
public class Scrubbable
{

    /// <summary>
    /// The exception that caused the mission to be scrubbed.
    /// </summary>
    public Exception? E;

    /// <summary>
    /// Flag to indicate to scrub the mission due to an unexpected failure.
    /// </summary>
    public bool Scrub;

    public Scrubbable()
    {
        E = null;
        Scrub = false;
    }

    public void ActivateScrub(Exception e)
    {
        E = e;
        Scrub = true;
    }

    public void ManuallyScrub(string message)
    {
        E = new Exception(message);
        Scrub = true;
    }

    public Exception GetException()
    {
        if (E == null)
        {
            if (Scrub)
            {
                return new Exception("An unknown exception caused a scrub.");
            }
            else
            {
                return new Exception("No exception appears to have occured but the caller wanted an exception.");
            }
        }
        else
        {
            return E;
        }
    }
}
namespace luminary.util;


/// <summary>
/// Helper class for async returns to let their caller know to panic.
/// </summary>
public class Panicable<T> : Panicable
{
    public T? ReturnValue;

    public Panicable(T? t = default) : base()
    {
        ReturnValue = t;
    }
}

/// <summary>
/// Helper class for async returns to let their caller know to panic.
/// </summary>
public class Panicable
{

    public Exception? E;

    public bool Paniced;

    public Panicable()
    {
        E = null;
        Paniced = false;
    }

    public void ActivatePanic(Exception e)
    {
        E = e;
        Paniced = true;
    }

    public void ManuallyPanic(string message)
    {
        E = new Exception(message);
        Paniced = true;
    }

    public Exception GetException()
    {
        if(E == null)
        {
            if(Paniced)
            {
                return new Exception("An unknown exception caused a panic.");
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
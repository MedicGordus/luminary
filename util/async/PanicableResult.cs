namespace luminary.util;


/// <summary>
/// Helper class for async returns, to handle panicing and logging.
/// </summary>
public class PanicableResult<T> : Panicable<T>
{
    private List<string> Results;

    public PanicableResult(T? _t = default) : base(_t)
    {
        Results = [];
    }

    public void AddResult(string _result)
    {
        Results.Add(_result);
    }

    public IEnumerable<string> GetResults() => Results;
}

/// <summary>
/// Helper class for async returns, to handle panicing and logging.
/// </summary>
public class PanicableResult : Panicable
{
    private List<string> Results;

    public PanicableResult() : base()
    {
        Results = [];
    }

    public void AddResult(string _result)
    {
        Results.Add(_result);
    }

    public void AddResults(IEnumerable<string> _results)
    {
        Results.AddRange(_results);
    }

    public IEnumerable<string> GetResults() => Results;
}
namespace luminary.util;


/// <summary>
/// Helper class for async returns, to handle panicing and logging.
/// </summary>
public class ScrubbableResult<T> : Scrubbable<T>
{
    private List<string> Results;

    public ScrubbableResult(T? _t = default) : base(_t)
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

/// <summary>
/// Helper class for async returns, to handle panicing and logging.
/// </summary>
public class ScrubbableResult : Scrubbable
{
    private List<string> Results;

    public ScrubbableResult() : base()
    {
        Results = [];
    }

    public void AddResult(string _result)
    {
        Results.Add(_result);
    }

    public IEnumerable<string> GetResults() => Results;
}
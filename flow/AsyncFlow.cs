using luminary.mapping;

namespace luminary.flow;


public abstract class AsyncFlow
{
    public abstract Task<Prism?> ProcessAsync(Prism _input);
}
using luminary.mapping;
using luminary.util;

namespace luminary.flow;


public abstract class AsyncFlow
{
    public abstract Task<Panicable<Prism?>> ProcessAsync(Prism _input);
}
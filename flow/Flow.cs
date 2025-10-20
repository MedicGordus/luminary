using luminary.mapping;

namespace luminary.flow;


public abstract class Flow
{
    public abstract Prism? Process(Prism _input);
}
namespace luminary.mapping;

public class MappingStage
{
    public Dictionary<ulong, Mapper> MappersById;

    public MappingStage(Dictionary<ulong, Mapper> _mappersById)
    {
        MappersById = _mappersById;
    }
}
using System.Text.Json;

using luminary.mapping.functions;

namespace luminary.mapping;

public class Prism
{
    public PrismOperator Payload;

    public SchemaJson? PrismSchema;

    public Prism (PrismOperator _payload, SchemaJson? _prismSchema)
    {
        Payload = _payload;
        PrismSchema = _prismSchema;
    }

    public string? BuildJsonStringPayload()
    {
        return Helper.ConvertPrismOperatorToJsonString(Payload.GetValue());
    }
}
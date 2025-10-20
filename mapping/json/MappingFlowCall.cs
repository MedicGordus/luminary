using System.Text.Json;
using System.Text.Json.Serialization;


namespace luminary.mapping;

/// <summary>
/// Function = (luminary.flow.FlowType).
/// 
/// Parameters = the inputs for the function.
/// 
/// Intended to work with Mapper.
/// </summary>
public class MappingFlowCallJson
{
    /// <summary>
    /// Option from (luminary.flow.FlowType).
    /// </summary>
    [JsonPropertyName("function")]
    public string? Function { get; set; }

    /// <summary>
    /// List of parameters for the corresponding (luminary.flow.FlowType) function.
    /// </summary>
    [JsonPropertyName("parameters")]
    public List<string>? Parameters { get; set; }
}
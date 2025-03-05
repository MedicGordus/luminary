using System.Text.Json.Serialization;


namespace luminary.mapping;

/// <summary>
/// This class represents a single action to take within a mapping step.
/// </summary>
public class ParameterMapJson
{
    /// <summary>
    /// The parameter on the output that will be filled.
    /// 
    /// Example: "output"."person"."first-name"
    /// </summary>
    [JsonPropertyName("output-parameter")]
    public string? OutputParameter { get; set; }

    /// <summary>
    /// The function that will be executed to fill the output parameter, this includes input parameters, logic operators, etc.
    /// 
    /// Examples:
    ///     "input"."person"."first-name"
    ///     trim("input"."person"."first-name")
    ///     concatenate(trim("input"."person"."first-name"),string(" "),trim("input"."person"."last-name"))
    /// </summary>
    [JsonPropertyName("function")]
    public string? Function { get; set; }
}
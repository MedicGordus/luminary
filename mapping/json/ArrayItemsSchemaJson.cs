using System.Text.Json.Serialization;

namespace luminary.mapping;

public class ArrayItemsSchemaJson
{
    /// <summary>
    /// Type in the array.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// (Official) This is used if the array is of strings.
    /// 
    /// Formats allowed (some are custom, some are official):
    ///     See OperatorValue.OperatorValueType
    ///     See OperatorValue.OperatorValueTypeLookup
    /// </summary>
    [JsonPropertyName("format")]
    public string? Format { get; set; }

    /// <summary>
    /// Used if the array is an array of objects.
    /// </summary>
    [JsonPropertyName("object-schema")]
    public SchemaJson? ObjectSchema { get; set; }
    

    /// <summary>
    /// Used if the array is an array of arrays.
    /// </summary>
    [JsonPropertyName("items")]
    public ArrayItemsSchemaJson? Items { get; set; }
}

using System.Text.Json;
using System.Text.Json.Serialization;

namespace luminary.mapping;

public class SchemaJson
{
    public readonly struct JsonTypes
    {
        public const string STRING = "string";
        public const string NUMBER = "number";
        public const string INTEGER = "integer";
        public const string OBJECT = "object";
        public const string ARRAY = "array";
        public const string BOOLEAN = "boolean";
        public const string NULL = "null";
    }

    [JsonPropertyName("title")]
    public string? Title { get; set; }


    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// (Official) Type of property.
    /// 
    /// Types allowed:
    ///     string
    ///     number
    ///     integer
    ///     object
    ///     array
    ///     boolean
    ///     null
    /// </summary>
    /// <remarks>
    /// Officially, json schema type could be a string or an array of options. For our case, we will enforce only single types.
    /// </remarks>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// (Official) This is used by strings.
    /// 
    /// Formats allowed (some are custom, some are official):
    ///     See OperatorValue.OperatorValueType
    ///     See OperatorValue.OperatorValueTypeLookup
    /// </summary>
    [JsonPropertyName("format")]
    public string? Format { get; set; }

    /// <summary>
    /// (Unofficial) Used to indicate what the array type is (for arrays), in conjunction with ArrayFormat.
    /// 
    /// Same values as Type, see Type.
    /// </summary>
    [JsonPropertyName("array-type")]
    public string? ArrayType { get; set; }

    /// <summary>
    /// (Unofficial) Used to indicate what the array format is (for arrays), in conjunction with ArrayType.
    /// 
    /// Same values as Format, see Format.
    /// </summary>
    [JsonPropertyName("array-format")]
    public string? ArrayFormat { get; set; }

    /// <summary>
    /// (Official) Properties expected within this schema.
    /// </summary>
    [JsonPropertyName("properties")]
    public Dictionary<string, SchemaJson>? Properties { get; set; }


    public static SchemaJson? Build(string? _json)
    {
        if(_json == null)
        {
            return null;
        }

        return JsonSerializer.Deserialize<SchemaJson>(_json);
    }
}
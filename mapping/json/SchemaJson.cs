
using System.Text.Json;
using System.Text.Json.Serialization;

namespace luminary.mapping;

/// <summary>
/// Loosely follows the json-schema.org definitions.
/// 
/// Does not comply with the $schema and $id rules.
/// 
/// For help, see:
///     https://json-schema.org/learn/getting-started-step-by-step
/// 
/// </summary>
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

    /// <summary>
    /// State the intent of the schema.
    /// 
    /// This keyword doesn't add any constraints to the data being validated.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }


    /// <summary>
    /// State the intent of the schema.
    /// 
    /// This keyword doesn't add any constraints to the data being validated.
    /// </summary>
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
    /// Officially, json schema type could be (A) a string (what type it is) or (B) an array of multiple types it could be.
    /// 
    /// For our case, we will enforce only single types.
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
    /// Used if the type is array.
    /// </summary>
    [JsonPropertyName("items")]
    public ArrayItemsSchemaJson? Items { get; set; }

    /// <summary>
    /// (Official) Properties expected within this schema.
    /// </summary>
    [JsonPropertyName("properties")]
    public Dictionary<string, SchemaJson>? Properties { get; set; }


    public static SchemaJson? Build(string? _json)
    {
        if (_json == null)
        {
            return null;
        }

        return JsonSerializer.Deserialize<SchemaJson>(_json);
    }
}

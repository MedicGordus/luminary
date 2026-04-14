using System.Text.Json.Serialization;
using luminary.mapping;
using luminary.mapping.functions;

namespace luminary.flow;

public class FileRetrievalFlowInputSchemaJson
{

    /// <summary>
    /// Path the file which is being retrieved.
    /// </summary>
    [JsonPropertyName(FILE_PATH_JSON_NAME)]
    public string? Path { get; set; }
    public const string FILE_PATH_JSON_NAME = "path";

    public static FileRetrievalFlowInputSchemaJson BuildFromPrism(Prism _input)
    {
        var basePrism = _input.Payload.GetValue() ?? throw new Exception(
            "Cannot build FileRetrievalFlowInputSchemaJson from prism as the getvalue call returned null."
        );
        if (!basePrism.TryGetValue(FILE_PATH_JSON_NAME, out var filePath) || filePath == null || filePath is not StringOperator filePathString)
        {
            throw new Exception($"Cannot build FileRetrievalFlowInputSchemaJson from prism as the file path '{FILE_PATH_JSON_NAME}' was missing, null, or was not a string.");
        }

        return new()
        {
            Path = filePathString.GetValue()
        };
    }
}